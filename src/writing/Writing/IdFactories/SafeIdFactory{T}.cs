namespace TNO.Logging.Writing.IdFactories;

internal class SafeIdFactory<T> where T : notnull
{
   #region Fields
   private readonly Dictionary<T, ulong> _idCache = new Dictionary<T, ulong>();
   private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
   private ulong _nextId;
   #endregion
   public SafeIdFactory(ulong startId)
   {
      _nextId = startId;
   }

   #region Methods
   /// <summary>Gets or creates the <paramref name="id"/> for the given <paramref name="data"/>.</summary>
   /// <param name="data">The data to get or create the <paramref name="id"/> for.</param>
   /// <param name="id">The id of the given <paramref name="data"/>.</param>
   /// <returns>
   /// <see langword="true"/> if a new <paramref name="id"/> 
   /// had to be created, <see langword="false"/> otherwise.
   /// </returns>
   public bool GetOrCreate(T data, out ulong id)
   {
      _cacheLock.EnterUpgradeableReadLock();
      try
      {
         if (_idCache.TryGetValue(data, out id))
            return false;

         _cacheLock.EnterWriteLock();
         try
         {
            id = _nextId;
            _nextId++;

            _idCache.Add(data, id);

            return true;
         }
         finally
         {
            _cacheLock.ExitWriteLock();
         }
      }
      finally
      {
         _cacheLock.ExitUpgradeableReadLock();
      }
   }
   #endregion
}