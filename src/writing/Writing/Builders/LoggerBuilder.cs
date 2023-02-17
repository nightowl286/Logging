using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.Collectors;
using TNO.Logging.Writing.Loggers;

namespace TNO.Logging.Writing.Builders;

internal sealed class LoggerBuilder : ILoggerBuilder
{
   #region Fields
   private readonly LogDataDistributor _distributor = new LogDataDistributor();
   private readonly LogWriterContext _context = new LogWriterContext();
   #endregion

   #region Properties
   /// <inheritdoc/>
   public ILogWriterFacade Facade { get; }
   public ILogger Logger { get; }
   #endregion
   public LoggerBuilder(ILogWriterFacade facade)
   {
      Facade = facade;
      Logger = new ScopedLogger(_distributor, _context, 0, 0);
   }

   #region Methods
   public ILoggerBuilder With(ILogDataCollector collector)
   {
      _distributor.Assign(collector);

      return this;
   }
   public ILogger Build(out ILogDataDistributor distributor)
   {
      distributor = _distributor;

      return Logger;
   }
   #endregion
}
