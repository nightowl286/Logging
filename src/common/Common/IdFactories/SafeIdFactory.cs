namespace TNO.Logging.Common.IdFactories;

/// <summary>
/// Represents a thread safe id factory.
/// </summary>
public class SafeIdFactory
{
   #region Fields
   private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
   private ulong _nextId;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="SafeIdFactory"/>.</summary>
   /// <param name="startId">The first id that will be given out.</param>
   public SafeIdFactory(ulong startId)
   {
      _nextId = startId;
   }
   #endregion

   #region Methods
   /// <summary>Gets the next id.</summary>
   /// <returns>The next id.</returns>
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