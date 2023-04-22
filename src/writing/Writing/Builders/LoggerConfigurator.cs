using System.Reflection;
using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Abstractions;
using TNO.Logging.Abstractions.Scopes;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.Contexts;
using TNO.Logging.Logging;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Exceptions;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Collectors;
using TNO.Logging.Writing.Exceptions;
using TNO.Logging.Writing.Serialisers;
using TNO.Logging.Writing.Serialisers.Registrants;

namespace TNO.Logging.Writing.Builders;

internal sealed class LoggerConfigurator : ILoggerConfigurator
{
   #region Fields
   private readonly IServiceScope _scope;
   private readonly LogWriteContext _context = new LogWriteContext();

   private readonly ExceptionDataHandlerRegistrar _exceptionDataHandlerRegistrar;
   private readonly ExceptionDataHandler _exceptionDataHandler;
   private readonly IExceptionInfoHandler _exceptionInfoHandler;

   private bool _includeInternalLogger = false;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public ILogDataDistributor Distributor { get; }

   /// <inheritdoc/>
   public ISerialiser Serialiser { get; }
   #endregion
   public LoggerConfigurator()
   {
      _scope = new ServiceFacade().CreateNew();

      Distributor = new LogDataDistributor();
      Serialiser = new Serialiser(_scope.Requester);

      _exceptionDataHandlerRegistrar = new ExceptionDataHandlerRegistrar();

      _exceptionDataHandler = new ExceptionDataHandler(_exceptionDataHandlerRegistrar, _scope.Builder, _context, Distributor);
      _exceptionInfoHandler = new ExceptionInfoHandler(Serialiser, _context, Distributor, _exceptionDataHandler);

      _scope.Registrar
         .Instance<ILogWriteContext>(_context)
         .Instance<ILogDataCollector>(Distributor)
         .Instance<IExceptionDataHandler>(_exceptionDataHandler)
         .Instance(_exceptionInfoHandler)
         .Instance(Serialiser);

      WithRegistrant(new BuiltinSerialiserRegistrant());
      WithRegistrant(new BuiltinExceptionDataHandlerRegistrant());
   }

   #region Methods
   /// <inheritdoc/>
   public ILoggerConfigurator With(ILogDataCollector collector)
   {
      Distributor.Assign(collector);
      return this;
   }

   /// <inheritdoc/>
   public ILoggerConfigurator WithInternalLogger(bool includeInternalLogger)
   {
      _includeInternalLogger = includeInternalLogger;
      return this;
   }

   /// <inheritdoc/>
   public ILoggerConfigurator WithRegistrant(ISerialiserRegistrant registrant)
   {
      registrant.Register(_scope);
      return this;
   }

   /// <inheritdoc/>
   public ILoggerConfigurator WithRegistrant(IExceptionDataHandlerRegistrant registrant)
   {
      registrant.Register(_exceptionDataHandlerRegistrar, _scope);
      return this;
   }

   /// <inheritdoc/>
   public IContextLogger Create()
   {
      ILogger internalLogger = CreateInternalLogger(_exceptionInfoHandler);
      _exceptionDataHandler.SetInternalLogger(internalLogger);

      IContextLogger mainLogger = new ContextLogger(Distributor, _context, _exceptionInfoHandler, 0, internalLogger);

      LogInitialInformation(internalLogger);

      return mainLogger;
   }
   private ILogger CreateInternalLogger(IExceptionInfoHandler exceptionInfoConverter)
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
      public void Deposit(DataKindVersion dataKindVersion) { }
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
      public bool ShouldLogVersionForDataKind(VersionedDataKind dataKind) => default;
      #endregion
   }
   #endregion
}
