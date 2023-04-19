namespace TNO.Logging.Abstractions.Scopes;

/// <summary>
/// Denotes a creator for logger scopes.
/// </summary>
public interface ILoggerScopeCreator
{
   #region Methods
   /// <summary>Creates a new logging scope.</summary>
   /// <returns>A new logger bound to the newly created scope.</returns>
   ILogger CreateScoped();
   #endregion
}
