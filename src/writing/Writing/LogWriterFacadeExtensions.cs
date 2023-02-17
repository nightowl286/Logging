using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Builders;

namespace TNO.Logging.Writing;

/// <summary>
/// Contains useful extension methods related to the <see cref="ILogWriterFacade"/>.
/// </summary>
public static class LogWriterFacadeExtensions
{
   #region Methods
   /// <summary>Creates a new logger configurator using the given <paramref name="facade"/>.</summary>
   /// <param name="facade">The facade to create the configurator with.</param>
   /// <returns>The created logger configurator.</returns>
   public static ILoggerConfigurator CreateConfigurator(this ILogWriterFacade facade)
   {
      LoggerConfigurator builder = new LoggerConfigurator(facade);

      return builder;
   }
   #endregion
}
