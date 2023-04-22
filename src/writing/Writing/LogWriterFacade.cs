using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Builders;

namespace TNO.Logging.Writing;

/// <summary>
/// Represents a facade for the log writing system.
/// </summary>
public class LogWriterFacade : ILogWriterFacade
{
   #region Methods
   /// <inheritdoc/>
   public ILoggerConfigurator CreateConfigurator()
   {
      LoggerConfigurator builder = new LoggerConfigurator();

      return builder;
   }
   #endregion
}