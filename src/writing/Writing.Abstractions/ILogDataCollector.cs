using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;

namespace TNO.Logging.Writing.Abstractions;

/// <summary>
/// Denotes a collector of log data.
/// </summary>
public interface ILogDataCollector
{
   #region Methods
   /// <summary>Deposits <paramref name="entry"/> data.</summary>
   /// <param name="entry">The entry to deposit.</param>
   void Deposit(IEntry entry);

   /// <summary>Deposits <paramref name="fileReference"/> data.</summary>
   /// <param name="fileReference">The file reference to deposit.</param>
   void Deposit(FileReference fileReference);
   #endregion
}
