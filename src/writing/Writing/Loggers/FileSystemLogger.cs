using TNO.Logging.Writing.Abstractions.Loggers;

namespace TNO.Logging.Writing.Loggers;

internal class FileSystemLogger : DisposableLogger, IFileSystemLogger
{
   #region Properties
   public string LogPath { get; }
   #endregion
   public FileSystemLogger(ILogger logger, IDisposable toDispose, string logPath) : base(logger, toDispose)
   {
      LogPath = logPath;
   }
}
