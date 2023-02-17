namespace TNO.Logging.Writing.Abstractions.Loggers.Scopes;

/// <summary>
/// Denotes a logger bound to a context.
/// </summary>
public interface IContextLogger : ILogger, ILoggerContextCreator, ILoggerScopeCreator
{
}
