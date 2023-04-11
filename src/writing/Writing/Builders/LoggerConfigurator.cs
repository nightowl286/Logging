using System.Reflection;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.LogData;
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
   public ILoggerConfigurator With(ILogDataCollector collector)
   {
      Distributor.Assign(collector);

      return this;
   }

   public IContextLogger Create()
   {
      ILogger internalLogger = CreateInternalLogger();

      IContextLogger mainLogger = new ContextLogger(Distributor, _context, 0, internalLogger);

      LogInitialInformation(internalLogger);

      return mainLogger;
   }
   private ILogger CreateInternalLogger()
   {
      ulong internalContextId = _context.CreateContextId();

      ContextInfo internalContext = new ContextInfo(CommonContexts.Internal, internalContextId, 0, 0, 0);
      Distributor.Deposit(internalContext);

      return new BaseLogger(Distributor, _context, internalContextId, 0);
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
}
