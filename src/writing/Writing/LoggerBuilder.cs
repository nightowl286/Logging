using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.Loggers;

namespace TNO.Logging.Writing;

internal sealed class LoggerBuilder : ILoggerBuilder
{
   #region Fields
   private readonly LogDataDistributor _distributor = new LogDataDistributor();
   private readonly LogWriterContext _context = new LogWriterContext();
   #endregion

   #region Properties
   /// <inheritdoc/>
   public ILogWriterFacade Facade { get; }
   #endregion
   public LoggerBuilder(ILogWriterFacade facade) => Facade = facade;

   #region Methods
   public ILoggerBuilder With(ILogDataCollector collector)
   {
      _distributor.Assign(collector);

      return this;
   }
   public ILogger Build(out ILogDataDistributor distributor)
   {
      distributor = _distributor;

      ScopedLogger logger = new ScopedLogger(distributor, _context);

      return logger;
   }
   #endregion
}
