using TNO.Logging.Common.Abstractions;

namespace TNO.Logging.Reading.Abstractions;

/// <summary>
/// Denotes a facade for the log reading system.
/// </summary>
public interface ILogReaderFacade
{
   #region Methods
   /// <summary>Generates a <see cref="IDeserialiserProvider"/> based on the given <paramref name="map"/>.</summary>
   /// <param name="map">The version map to use.</param>
   /// <returns>The generated <see cref="IDeserialiserProvider"/>.</returns>
   IDeserialiserProvider GenerateProvider(DataVersionMap map);
   #endregion
}
