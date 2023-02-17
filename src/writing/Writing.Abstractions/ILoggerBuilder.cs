using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Loggers.Scopes;

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
   /// <remarks>Can be used instead of calling <see cref="Build(out ILogDataDistributor)"/>.</remarks>
   IContextLogger Logger { get; }
   #endregion

   #region Methods
   /// <summary>Adds the given <paramref name="collector"/> to the distributor that will be used.</summary>
   /// <param name="collector">The collector to add.</param>
   /// <returns>The current logger builder.</returns>
   ILoggerBuilder With(ILogDataCollector collector);

   /// <summary>Builds the requested logger.</summary>
   /// <param name="distributor">The internal distributor that was created.</param>
   /// <returns>The built <see cref="Logger"/>.</returns>
   IContextLogger Build(out ILogDataDistributor distributor);
   #endregion
}
