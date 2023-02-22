using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.LogData;

namespace TNO.Logging.Writing.Abstractions.Collectors;

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

   /// <summary>Deposits <paramref name="contextInfo"/> data.</summary>
   /// <param name="contextInfo">The context info to deposit.</param>
   void Deposit(ContextInfo contextInfo);

   /// <summary>Deposits <paramref name="tagReference"/> data.</summary>
   /// <param name="tagReference">The tag reference to deposit.</param>
   void Deposit(TagReference tagReference);
   #endregion
}
