using TNO.Logging.Reading.Abstractions;
using TNO.Logging.Reading.Builders;

namespace TNO.Logging.Reading;

/// <summary>
/// Represents a facade for the log reading system.
/// </summary>
public class LogReaderFacade : ILogReaderFacade
{
   #region Methods
   /// <inheritdoc/>
   public ILogReaderConfigurator CreateConfigurator()
   {
      LogReaderConfigurator configurator = new LogReaderConfigurator();

      return configurator;
   }
   #endregion
}