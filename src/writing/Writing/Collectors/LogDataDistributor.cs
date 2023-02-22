using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Abstractions.Collectors;

namespace TNO.Logging.Writing.Collectors;

/// <summary>
/// Represents a distributor of log data.
/// </summary>
public sealed class LogDataDistributor : ILogDataDistributor
{
   #region Fields
   private readonly HashSet<ILogDataCollector> _collectors = new HashSet<ILogDataCollector>();
   private readonly ReaderWriterLockSlim _collectorsLock = new ReaderWriterLockSlim();
   #endregion

   #region Methods
   /// <inheritdoc/>
   public bool Assign(ILogDataCollector collector)
   {
      _collectorsLock.EnterWriteLock();
      try
      {
         return _collectors.Add(collector);
      }
      finally
      {
         _collectorsLock.ExitWriteLock();
      }
   }
   /// <inheritdoc/>
   public bool IsAssigned(ILogDataCollector collector)
   {
      _collectorsLock.EnterReadLock();
      try
      {
         return _collectors.Contains(collector);
      }
      finally
      {
         _collectorsLock.ExitReadLock();
      }
   }

   /// <inheritdoc/>
   public bool Unassign(ILogDataCollector collector)
   {
      _collectorsLock.EnterWriteLock();
      try
      {
         return _collectors.Remove(collector);
      }
      finally
      {
         _collectorsLock.ExitWriteLock();
      }
   }
   #endregion

   #region Collector Methods
   /// <inheritdoc/>
   public void Deposit(IEntry entry)
   {
      foreach (ILogDataCollector collector in EnumerateCollectors())
         collector.Deposit(entry);
   }

   /// <inheritdoc/>
   public void Deposit(FileReference fileReference)
   {
      foreach (ILogDataCollector collector in EnumerateCollectors())
         collector.Deposit(fileReference);
   }

   /// <inheritdoc/>
   public void Deposit(ContextInfo contextInfo)
   {
      foreach (ILogDataCollector collector in EnumerateCollectors())
         collector.Deposit(contextInfo);
   }

   /// <inheritdoc/>
   public void Deposit(TagReference tagReference)
   {
      foreach (ILogDataCollector collector in EnumerateCollectors())
         collector.Deposit(tagReference);
   }
   #endregion

   #region Helpers
   private IEnumerable<ILogDataCollector> EnumerateCollectors()
   {
      _collectorsLock.EnterReadLock();
      try
      {
         foreach (ILogDataCollector collector in _collectors)
            yield return collector;
      }
      finally
      {
         _collectorsLock.ExitReadLock();
      }
   }


   #endregion
}
