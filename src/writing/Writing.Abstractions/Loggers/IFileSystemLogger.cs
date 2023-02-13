namespace TNO.Logging.Writing.Abstractions.Loggers;

/// <summary>
///  Denotes a logger that will save the log to the file system.
/// </summary>
public interface IFileSystemLogger : IDisposableLogger
{
   #region Properties
   /// <summary>The path that the log will be saved to.</summary>
   string LogPath { get; }
   #endregion
}
