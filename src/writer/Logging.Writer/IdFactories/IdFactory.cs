namespace TNO.Logging.Writer.IdFactories;
internal class IdFactory
{
   #region Fields
   private ulong _nextId;
   #endregion
   public IdFactory(ulong startId) => _nextId = startId;

   #region Methods
   public virtual ulong GetId()
   {
      ulong id = _nextId;
      _nextId++;
      return id;
   }
   #endregion
}
