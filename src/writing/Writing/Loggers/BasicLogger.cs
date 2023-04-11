using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Loggers;

namespace TNO.Logging.Writing.Loggers;

/// <summary>
/// Represents a scoped logger.
/// </summary>
public class BasicLogger : BaseLogger
{
   #region Properties
   internal ILogger InternalLogger { get; }
   #endregion

   #region Constructors
   internal BasicLogger(ILogDataCollector collector, ILogWriteContext writeContext, ulong contextId, ulong scope, ILogger internalLogger)
      : base(collector, writeContext, contextId, scope)
   {
      InternalLogger = internalLogger;
   }
   #endregion

}
