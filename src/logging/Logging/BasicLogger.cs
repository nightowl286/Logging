using TNO.Logging.Abstractions;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Exceptions;

namespace TNO.Logging;

/// <summary>
/// Represents a scoped logger.
/// </summary>
public class BasicLogger : BaseLogger
{
   #region Properties
   internal ILogger InternalLogger { get; }
   #endregion

   #region Constructors
   internal BasicLogger(
      ILogDataCollector collector,
      ILogWriteContext writeContext,
      IExceptionInfoHandler exceptionInfoConverter,
      ulong contextId,
      ulong scope,
      ILogger internalLogger)
      : base(collector, writeContext, exceptionInfoConverter, contextId, scope)
   {
      InternalLogger = internalLogger;
   }
   #endregion

}
