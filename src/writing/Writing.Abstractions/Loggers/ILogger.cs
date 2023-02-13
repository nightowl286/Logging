using System.Runtime.CompilerServices;
using TNO.Logging.Common.Abstractions.Entries;

namespace TNO.Logging.Writing.Abstractions.Loggers;

/// <summary>
/// Denotes a logger.
/// </summary>
public interface ILogger
{
   #region Methods
   /// <summary>Writes the given <paramref name="message"/> to the log.</summary>
   /// <param name="Importance">
   /// The severity and purpose of the entry that will be created.
   /// This value should be normalised.
   /// </param>
   /// <param name="message">The message to write.</param>
   /// <param name="file">
   /// The file from which this method was called. 
   /// This should be provided by the compiler.
   /// </param>
   /// <param name="line">
   /// The line number in the <paramref name="file"/> where this method was called from.
   /// This should be provided by the compiler.
   /// </param>
   /// <returns>The logger that was used.</returns>
   ILogger Log(Importance Importance, string message,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0);
   #endregion
}