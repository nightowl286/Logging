using TNO.Logging.Writing.Abstractions;

namespace TNO.Logging.Writing;

/// <summary>
/// Contains useful extension methods related to the <see cref="ILogWriterFacade"/>.
/// </summary>
public static class LogWriterFacadeExtensions
{
   #region Methods
   /// <summary>Creates a new logger builder using the given <paramref name="facade"/>.</summary>
   /// <param name="facade">The facade to create the builder with.</param>
   /// <returns>The created logger builder.</returns>
   public static ILoggerBuilder CreateBuilder(this ILogWriterFacade facade)
   {
      LoggerBuilder builder = new LoggerBuilder(facade);

      return builder;
   }
   #endregion
}
