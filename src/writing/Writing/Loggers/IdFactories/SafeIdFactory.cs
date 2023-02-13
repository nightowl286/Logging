namespace TNO.Logging.Writing.Loggers.IdFactories;
internal class SafeIdFactory
{
   #region Fields
   private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
   private ulong _nextId;
   #endregion
   public SafeIdFactory(ulong startId)
   {
      _nextId = startId;
   }

   #region Methods
   public ulong GetNext()
   {
      _semaphore.Wait();
      try
      {
         ulong id = _nextId;
         _nextId++;

         return id;
      }
      finally
      {
         _semaphore.Release();
      }
   }
   #endregion
}