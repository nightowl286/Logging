namespace TNO.Logging.Writer.IdFactories;
internal class SafeIdFactory : IdFactory
{
   #region Fields
   private readonly SemaphoreSlim _lock = new SemaphoreSlim(1);
   #endregion

   public SafeIdFactory(ulong startId) : base(startId) { }

   #region Methods
   public override ulong GetId()
   {
      {
         _lock.Wait();
         try
         {
            ulong id = base.GetId();

            return id;
         }
         finally
         {
            _lock.Release();
         }
      }
   }
   #endregion
}
