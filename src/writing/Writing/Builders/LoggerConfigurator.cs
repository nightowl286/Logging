using System.Reflection;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.Contexts;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.Abstractions.Loggers.Scopes;
using TNO.Logging.Writing.Collectors;
using TNO.Logging.Writing.Loggers;

namespace TNO.Logging.Writing.Builders;

internal sealed class LoggerConfigurator : ILoggerConfigurator
{
   #region Fields
   private readonly LogWriteContext _context = new LogWriteContext();
   private bool _includeInternalLogger = false;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public ILogWriterFacade Facade { get; }

   /// <inheritdoc/>
   public ILogDataDistributor Distributor { get; }
   #endregion
   public LoggerConfigurator(ILogWriterFacade facade)
   {
      Facade = facade;
      Distributor = new LogDataDistributor();
   }

   #region Methods
   /// <inheritdoc/>
   public ILoggerConfigurator With(ILogDataCollector collector)
   {
      Distributor.Assign(collector);

      return this;
   }

   /// <inheritdoc/>
   public ILoggerConfigurator DisableInternalLogger()
   {
      _includeInternalLogger = false;
      return this;
   }

   /// <inheritdoc/>
   public IContextLogger Create()
   {
      ILogger internalLogger = CreateInternalLogger();

      IContextLogger mainLogger = new ContextLogger(Distributor, _context, 0, internalLogger);

      LogInitialInformation(internalLogger);

      return mainLogger;
   }

   /// <inheritdoc/>
   private ILogger CreateInternalLogger()
   {
      if (_includeInternalLogger)
      {
         ulong internalContextId = _context.CreateContextId();

         ContextInfo internalContext = new ContextInfo(CommonContexts.Internal, internalContextId, 0, 0, 0);
         Distributor.Deposit(internalContext);

         return new BaseLogger(Distributor, _context, internalContextId, 0);
      }
      else
      {
         VoidCollector collector = new VoidCollector();
         VoidWriteContext context = new VoidWriteContext();

         return new BaseLogger(collector, context, 0, 0);
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
      #endregion
   }
   #endregion
}
