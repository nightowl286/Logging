using System;

namespace TNO.Logging.Writing.Abstractions.Loggers;

/// <summary>
/// Denotes a logger that can be disposed.
/// </summary>
public interface IDisposableLogger : ILogger, IDisposable
{
}