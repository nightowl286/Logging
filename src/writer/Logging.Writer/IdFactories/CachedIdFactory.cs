namespace TNO.Logging.Writer.IdFactories;
internal class CachedIdFactory<T> where T : notnull
{
   #region Fields
   private readonly IdFactory _idFactory;
   private readonly Dictionary<T, ulong> _cachedIds = new Dictionary<T, ulong>();
   private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
   #endregion
   public CachedIdFactory(ulong startId) => _idFactory = new IdFactory(startId);

   #region Methods
   public ulong GetId(T key)
   {
      _lock.EnterUpgradeableReadLock();
      try
      {
         if (_cachedIds.TryGetValue(key, out ulong id) == false)
         {
            _lock.EnterWriteLock();
            try
            {
               id = _idFactory.GetId();
               _cachedIds.Add(key, id);
            }
            finally
            {
               _lock.ExitWriteLock();
            }
         }

         return id;
      }
      finally
      {
         _lock.ExitUpgradeableReadLock();
      }
   }
   #endregion
}
