using TNO.Logging.Common.Abstractions.Entries;

namespace TNO.Logging.Writing.Abstractions.Loggers;

/// <summary>
/// Denotes a logger.
/// </summary>
public interface ILogger
{
   #region Methods
   /// <summary>Writes the given <paramref name="message"/> to the log.</summary>
   /// <param name="severityAndPurpose">The severity and purpose of the entry that will be created.</param>
   /// <param name="message">The message to write.</param>
   /// <returns>The logger that was used.</returns>
   ILogger Log(SeverityAndPurpose severityAndPurpose, string message);
   #endregion
}
