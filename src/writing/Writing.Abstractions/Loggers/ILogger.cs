namespace TNO.Logging.Writing.Abstractions.Loggers;

/// <summary>
/// Denotes a logger.
/// </summary>
public interface ILogger
{
   #region Methods
   /// <summary>Writes the given <paramref name="message"/> to the log.</summary>
   /// <param name="message">The message to write.</param>
   /// <returns>The logger that was used.</returns>
   ILogger Log(string message);
   #endregion
}
