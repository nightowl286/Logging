using TNO.Logging.Writing.Abstractions.Loggers;

namespace TNO.Logging.Writing.Loggers;

internal class FileSystemLoggerWrapper : DisposableLogger, IFileSystemLogger
{
   #region Properties
   public string LogPath { get; }
   #endregion
   public FileSystemLoggerWrapper(ILogger logger, IDisposable toDispose, string logPath) : base(logger, toDispose)
   {
      LogPath = logPath;
   }
}
