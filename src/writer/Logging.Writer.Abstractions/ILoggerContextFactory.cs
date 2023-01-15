namespace TNO.Logging.Writer.Abstractions;

/// <summary>
/// Denotes that the implementing type is capable of creating new logger contexts.
/// </summary>
public interface ILoggerContextFactory
{
   #region Methods
   /// <summary>Creates a new context with the given <paramref name="name"/>.</summary>
   /// <param name="name">The name of the context.</param>
   /// <returns>The logger with the newly created context.</returns>
   ILogger CreateContext(string name);
   #endregion
}
