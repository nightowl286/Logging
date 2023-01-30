using TNO.Logging.Common.Abstractions.Entries;

namespace TNO.Logging.Writing.Abstractions.Loggers;

/// <summary>
/// Denotes a writer for the log files.
/// </summary>
public interface ILogWriter
{
   #region Methods
   /// <summary>Requests the given <paramref name="entry"/> to be written to the log.</summary>
   /// <param name="entry">The entry that should be written to the log.</param>
   void RequestWrite(IEntry entry);
   #endregion
}
