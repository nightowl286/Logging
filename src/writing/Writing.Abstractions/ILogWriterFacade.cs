namespace TNO.Logging.Writing.Abstractions;

/// <summary>
/// Denotes a facade for the log writing system.
/// </summary>
public interface ILogWriterFacade : ISerialiserProvider
{
   #region Methods
   /// <summary>Creates a new logger configurator using the current facade.</summary>
   /// <returns>The created logger configurator.</returns>
   ILoggerConfigurator CreateConfigurator();
   #endregion
}