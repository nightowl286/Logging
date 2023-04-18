using System.Reflection;
using System.Runtime.InteropServices;
using TNO.Common.Extensions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.Contexts;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Exceptions;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.Abstractions.Loggers.Scopes;
using TNO.Logging.Writing.Collectors;
using TNO.Logging.Writing.Exceptions;
using TNO.Logging.Writing.Loggers;

namespace TNO.Logging.Writing.Builders;

internal sealed class LoggerConfigurator : ILoggerConfigurator
{
   #region Fields
   private readonly LogWriteContext _context = new LogWriteContext();
   private readonly ExceptionGroupStore _exceptionGroupStore = new ExceptionGroupStore();
   private readonly IServiceScope _scope;
   private bool _includeInternalLogger = false;
   private IContextLogger? _mainLogger;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public ILogWriterFacade Facade { get; }

   /// <inheritdoc/>
   public ILogDataDistributor Distributor { get; }
   #endregion
   public LoggerConfigurator(ILogWriterFacade facade, IServiceScope scope)
   {
      _scope = scope;
      Facade = facade;
      Distributor = new LogDataDistributor();
   }

   #region Methods
   /// <inheritdoc/>
   public ILoggerConfigurator With(ILogDataCollector collector)
   {
      if (_mainLogger is null)
         throw new InvalidOperationException($"{nameof(WithExceptions)} must be called before the first collector is added with the ({nameof(With)}) method.");

      Distributor.Assign(collector);
      return this;
   }

   /// <inheritdoc/>
   public ILoggerConfigurator DisableInternalLogger()
   {
      if (_mainLogger is not null)
         throw new InvalidOperationException($"{nameof(DisableInternalLogger)} can only be called before {nameof(WithExceptions)}.");
      _includeInternalLogger = false;
      return this;
   }

   /// <inheritdoc/>
   public IContextLogger Create()
   {
      if (_mainLogger is null)
         throw new InvalidOperationException($"{nameof(WithExceptions)} must be called before {nameof(Create)} can be called.");

      return _mainLogger;
   }

   /// <inheritdoc/>
   private void WithExceptionsGroup(Type serialiserType, Type converterType)
   {
      List<Type> serialiserTypes = serialiserType.GetOpenInterfaceImplementations(typeof(IExceptionDataSerialiser<>)).ToList();
      List<Type> converterTypes = converterType.GetOpenInterfaceImplementations(typeof(IExceptionDataConverter<,>)).ToList();

      if (serialiserTypes.Count > 1)
         throw new ArgumentException($"The given serialiser type ({serialiserType}) had multiple implementations.", nameof(serialiserType));
      else if (serialiserTypes.Count == 0)
         throw new ArgumentException($"The given serialiser type ({serialiserType}) had no valid implementations.", nameof(serialiserType));

      if (converterTypes.Count > 1)
         throw new ArgumentException($"The given converter type ({converterType}) had multiple implementations.", nameof(converterType));
      else if (converterTypes.Count == 0)
         throw new ArgumentException($"The given converter type ({converterType}) had no valid implementations.", nameof(converterType));

      string? serialiserGuidString = serialiserType.GetCustomAttribute<GuidAttribute>()?.Value;
      string? converterGuidString = converterType.GetCustomAttribute<GuidAttribute>()?.Value;

      if (serialiserGuidString is null) throw new ArgumentException($"The given serialiser type ({serialiserType}) did not have the {typeof(GuidAttribute)}.", nameof(serialiserType));
      if (converterGuidString is null) throw new ArgumentException($"The given converter type ({converterType}) did not have the {typeof(GuidAttribute)}.", nameof(converterType));

      Guid serialiserGuid = new Guid(serialiserGuidString);
      Guid converterGuid = new Guid(converterGuidString);

      if (serialiserGuid != converterGuid)
         throw new ArgumentException($"The guid ({serialiserGuid}) on the given serialiser type ({serialiserType}) did not " +
            $"match the guid ({converterGuid}) on the given converter type ({converterType}).", nameof(serialiserType));

      Type[] serialiserGenerics = serialiserTypes[0].GetGenericArguments();
      Type[] converterGenerics = converterTypes[0].GetGenericArguments();

      Type exceptionType = converterGenerics[0];

      Type serialiserDataType = serialiserGenerics[0];
      Type converterDataType = converterGenerics[1];

      if (serialiserDataType != converterDataType)
         throw new ArgumentException($"The exception data type ({serialiserDataType}) on the given serialiser type ({serialiserType})" +
            $" did not match the exception data type ({converterDataType}) on the given converter type ({converterType}).", nameof(serialiserType));

      _exceptionGroupStore.Add(serialiserType, converterType, exceptionType, converterDataType, serialiserGuid);
   }

   private void RegisterBaseExceptionSerialisers(ILogger internalLogger)
   {
      ExceptionDataConverterAndSerialiser exceptionDataHandler = new ExceptionDataConverterAndSerialiser(
         _exceptionGroupStore,
         _context,
         Distributor,
         internalLogger);

      _scope.Registrar
         .Instance<IExceptionDataConverter>(exceptionDataHandler)
         .Instance<IExceptionDataSerialiser>(exceptionDataHandler)
         .Singleton<IExceptionInfoSerialiser, ExceptionInfoSerialiser>()
         .Singleton<IExceptionInfoConverter, ExceptionInfoConverter>();
   }

   /// <inheritdoc/>
   public ILoggerConfigurator WithExceptions()
   {
      Assembly currentAssembly = Assembly.GetExecutingAssembly();
      foreach (Type type in currentAssembly.GetTypes())
      {
         const string namespacePrefix = "TNO.Logging.Writing.Exceptions";
         bool isExceptionGroupType =
            type.IsClass &&
            !type.IsInterface &&
            type.Namespace?.StartsWith(namespacePrefix) == true &&
            type.IsDefined<GuidAttribute>(false);

         if (isExceptionGroupType)
            WithExceptionsGroup(type, type);
      }

      SetupInternals();

      return this;
   }
   private void SetupInternals()
   {
      DelayedExceptionInfoConverter converter = new DelayedExceptionInfoConverter(_scope.Requester, _context, Distributor);

      ILogger internalLogger = CreateInternalLogger(converter);
      RegisterBaseExceptionSerialisers(internalLogger);

      IContextLogger mainLogger = new ContextLogger(Distributor, _context, converter, 0, internalLogger);

      LogInitialInformation(internalLogger);

      _mainLogger = mainLogger;
   }
   private ILogger CreateInternalLogger(IExceptionInfoConverter exceptionInfoConverter)
   {
      if (_includeInternalLogger)
      {
         ulong internalContextId = _context.CreateContextId();

         ContextInfo internalContext = new ContextInfo(CommonContexts.Internal, internalContextId, 0, 0, 0);
         Distributor.Deposit(internalContext);

         return new BaseLogger(Distributor, _context, exceptionInfoConverter, internalContextId, 0);
      }
      else
      {
         VoidCollector collector = new VoidCollector();
         VoidWriteContext context = new VoidWriteContext();

         return new BaseLogger(collector, context, exceptionInfoConverter, 0, 0);
      }
   }
   private static void LogInitialInformation(ILogger logger)
   {
      LogWriterAssembly(logger);
      LogEntryAssembly(logger);
   }
   private static void LogWriterAssembly(ILogger logger)
   {
      Assembly writerAssembly = Assembly.GetExecutingAssembly();

      logger
        .StartEntry(Severity.Negligible | Purpose.Diagnostics)
        .WithTag(CommonTags.WriterAssembly)
        .With(writerAssembly)
        .FinishEntry();
   }
   private static void LogEntryAssembly(ILogger logger)
   {
      Assembly? entryAssembly = Assembly.GetEntryAssembly();

      IEntryBuilder entryAssemblyBuilder = logger
         .StartEntry(Severity.Negligible | Purpose.Diagnostics)
         .WithTag(CommonTags.EntryAssembly);

      if (entryAssembly is null)
      {
         entryAssemblyBuilder
            .With($"Could not obtain the entry assembly, this likely means that the assembly has been loaded in from an unmanaged context.")
            .WithTable()
               .With("more-info", "https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly.getentryassembly#remarks")
               .BuildTable();
      }
      else
         entryAssemblyBuilder.With(entryAssembly);

      entryAssemblyBuilder.FinishEntry();
   }
   #endregion

   #region Stub classes
   private class DelayedExceptionInfoConverter : IExceptionInfoConverter
   {
      #region Fields
      private readonly object _lock = new object();
      private readonly IServiceRequester _requester;
      private readonly ILogWriteContext _writeContext;
      private readonly ILogDataCollector _dataCollector;
      private IExceptionInfoConverter? _converter;
      #endregion

      #region Constructors
      public DelayedExceptionInfoConverter(
         IServiceRequester requester,
         ILogWriteContext writeContext,
         ILogDataCollector dataCollector)
      {
         _requester = requester;
         _writeContext = writeContext;
         _dataCollector = dataCollector;
      }
      #endregion

      #region Methods
      public IExceptionInfo Convert(Exception exception, int? threadId)
      {
         IExceptionInfoConverter converter;
         lock (_lock)
         {
            _converter ??= new ExceptionInfoConverter(
                  _writeContext,
                  _dataCollector,
                  _requester.Get<IExceptionDataConverter>());

            converter = _converter;
         }

         return converter.Convert(exception, threadId);
      }
      #endregion
   }
   private class VoidCollector : ILogDataCollector
   {
      #region Methods
      public void Deposit(IEntry entry) { }
      public void Deposit(FileReference fileReference) { }
      public void Deposit(ContextInfo contextInfo) { }
      public void Deposit(TagReference tagReference) { }
      public void Deposit(TableKeyReference tableKeyReference) { }
      public void Deposit(AssemblyReference assemblyReference) { }
      public void Deposit(TypeReference typeReference) { }
      #endregion
   }
   private class VoidWriteContext : ILogWriteContext
   {
      #region Methods
      public ulong CreateContextId() => default;
      public bool GetOrCreateAssemblyId(AssemblyIdentity assemblyIdentity, out ulong assemblyId)
      {
         assemblyId = default;
         return default;
      }
      public bool GetOrCreateFileId(string file, out ulong fileId)
      {
         fileId = default;
         return default;
      }
      public bool GetOrCreateTableKeyId(string key, out uint tableKeyId)
      {
         tableKeyId = default;
         return default;
      }
      public bool GetOrCreateTagId(string tag, out ulong tagId)
      {
         tagId = default;
         return default;
      }
      public bool GetOrCreateTypeId(TypeIdentity typeIdentity, out ulong typeId)
      {
         typeId = default;
         return default;
      }
      public TimeSpan GetTimestamp() => default;
      public ulong NewEntryId() => default;
      public bool ShouldLogUnknownException(Type exceptionType) => default;
      #endregion
   }
   #endregion
}
