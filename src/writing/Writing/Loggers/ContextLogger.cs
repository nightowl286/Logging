using System.Runtime.CompilerServices;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.Abstractions.Loggers.Scopes;
using TNO.Logging.Writing.IdFactories;

namespace TNO.Logging.Writing.Loggers;

/// <summary>
/// Represents a logger bound to a context.
/// </summary>
public class ContextLogger : BasicLogger, IContextLogger
{
   #region Fields
   private readonly SafeIdFactory _scopeFactory = new SafeIdFactory(1);
   #endregion

   #region Constructors
   /// <summary>Creates a new logger that belongs to a specific context.</summary>
   /// <param name="collector">The collector that this logger should deposit data in.</param>
   /// <param name="writeContext">The write context to use.</param>
   /// <param name="contextId">The id of the context that this logger belongs to.</param>
   /// <param name="internalLogger">The internal logger to use.</param>
   internal ContextLogger(ILogDataCollector collector, ILogWriteContext writeContext, ulong contextId, ILogger internalLogger)
      : base(collector, writeContext, contextId, 0, internalLogger)
   {
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IContextLogger CreateContext(string name, [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      ulong id = WriteContext.CreateContextId();
      ulong fileId = GetFileId(file);

      ContextInfo contextInfo = new ContextInfo(name, id, ContextId, fileId, line);
      Collector.Deposit(contextInfo);

      return new ContextLogger(Collector, WriteContext, id, InternalLogger);
   }

   /// <inheritdoc/>
   public ILogger CreateScoped()
   {
      ulong scope = _scopeFactory.GetNext();

      return new BasicLogger(Collector, WriteContext, ContextId, scope, InternalLogger);
   }
   #endregion
}
