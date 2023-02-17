using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Loggers.Scopes;
using TNO.Logging.Writing.Collectors;
using TNO.Logging.Writing.Loggers;

namespace TNO.Logging.Writing.Builders;

internal sealed class LoggerConfigurator : ILoggerConfigurator
{
   #region Fields
   private readonly LogWriterContext _context = new LogWriterContext();
   #endregion

   #region Properties
   /// <inheritdoc/>
   public ILogWriterFacade Facade { get; }

   /// <inheritdoc/>
   public IContextLogger Logger { get; }

   /// <inheritdoc/>
   public ILogDataDistributor Distributor { get; }
   #endregion
   public LoggerConfigurator(ILogWriterFacade facade)
   {
      Facade = facade;
      Distributor = new LogDataDistributor();

      Logger = new ContextLogger(Distributor, _context, 0);
   }

   #region Methods
   public ILoggerConfigurator With(ILogDataCollector collector)
   {
      Distributor.Assign(collector);

      return this;
   }
   #endregion
}
