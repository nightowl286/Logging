using TNO.Logging.Writing.Abstractions.Loggers;

namespace TNO.Logging.Writing.Abstractions;

/// <summary>
/// Denotes a logger builder.
/// </summary>
public interface ILoggerBuilder
{
   #region Properties
   /// <summary>The facade that was used to create this builder.</summary>
   ILogWriterFacade Facade { get; }

   /// <summary>The logger that was built.</summary>
   ILogger Logger { get; }
   #endregion

   #region Methods
   /// <summary>Adds the given <paramref name="collector"/> to the distributor that will be used.</summary>
   /// <param name="collector">The collector to add.</param>
   /// <returns>The current logger builder.</returns>
   ILoggerBuilder With(ILogDataCollector collector);

   /// <summary>Builds the requested logger.</summary>
   /// <param name="distributor">The internal distributor that was created.</param>
   /// <returns>The built <see cref="Logger"/>.</returns>
   ILogger Build(out ILogDataDistributor distributor);
   #endregion
}
