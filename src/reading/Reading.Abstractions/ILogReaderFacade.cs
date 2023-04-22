namespace TNO.Logging.Reading.Abstractions;

/// <summary>
/// Denotes a facade for the log reading system.
/// </summary>
public interface ILogReaderFacade
{
   #region Methods
   /// <summary>Creates a new log reader configurator.</summary>
   /// <returns>The created logger configurator.</returns>
   ILogReaderConfigurator CreateConfigurator();
   #endregion
}