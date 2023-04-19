using TNO.Logging.Abstractions.Scopes;
using TNO.Logging.Writing.Abstractions.Collectors;

namespace TNO.Logging.Writing.Abstractions;

/// <summary>
/// Denotes a logger configurator.
/// </summary>
public interface ILoggerConfigurator
{
   #region Properties
   /// <summary>The facade that was used to create this configurator.</summary>
   ILogWriterFacade Facade { get; }

   /// <summary>The distributor that the created logger will use.</summary>
   ILogDataDistributor Distributor { get; }
   #endregion

   #region Methods
   /// <summary>Adds the given <paramref name="collector"/> to the distributor that will be used.</summary>
   /// <param name="collector">The collector to add.</param>
   /// <returns>The current logger configurator.</returns>
   ILoggerConfigurator With(ILogDataCollector collector);

   /// <summary>Voids any data logged to the internal logger.</summary>
   /// <returns>The current logger configurator.</returns>
   /// <remarks>Please only use this if you know what you are doing.</remarks>
   ILoggerConfigurator DisableInternalLogger();

   /// <summary>Registers the available exception serialisers and converters.</summary>
   /// <returns>The current logger configurator.</returns>
   ILoggerConfigurator WithExceptions();

   /// <summary>Created the final logger that should be used.</summary>
   /// <returns>The logger that should be used.</returns>
   IContextLogger Create();
   #endregion
}
