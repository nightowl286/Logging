using System;
using TNO.Logging.Writing.Abstractions.Collectors;

namespace TNO.Logging.Writing.Abstractions.Writers;

/// <summary>
/// Denotes a log writer that will save the log to the file system.
/// </summary>
public interface IFileSystemLogWriter : ILogDataCollector, IDisposable
{
   #region Properties
   /// <summary>The path where the log will be saved.</summary>
   string LogPath { get; }
   #endregion
}
