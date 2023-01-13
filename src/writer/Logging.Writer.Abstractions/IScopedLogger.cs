namespace TNO.Logging.Writer.Abstractions;

/// <summary>
/// Denotes a logger contained in a scope.
/// </summary>
public interface IScopedLogger : ILoggerBase
{
   #region Methods
   /// <summary>Creates a new scope in the current context, with an optional <paramref name="name"/>.</summary>
   /// <param name="name">The optional name for this scope.</param>
   /// <returns>The scoped instance of the logger.</returns>
   IScopedLogger CreateScope(string name = "");
   #endregion
}
