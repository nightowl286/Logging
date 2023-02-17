using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Loggers.Scopes;

namespace TNO.Logging.Writing.Abstractions;

/// <summary>
/// Denotes a logger configurator.
/// </summary>
public interface ILoggerConfigurator
{
   #region Properties
   /// <summary>The facade that was used to create this configurator.</summary>
   ILogWriterFacade Facade { get; }

   /// <summary>The logger that was built.</summary>
   IContextLogger Logger { get; }

   /// <summary>The distributor that the <see cref="Logger"/> will use.</summary>
   ILogDataDistributor Distributor { get; }
   #endregion

   #region Methods
   /// <summary>Adds the given <paramref name="collector"/> to the distributor that will be used.</summary>
   /// <param name="collector">The collector to add.</param>
   /// <returns>The current logger configurator.</returns>
   ILoggerConfigurator With(ILogDataCollector collector);
   #endregion
}
