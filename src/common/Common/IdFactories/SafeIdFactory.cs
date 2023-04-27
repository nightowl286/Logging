namespace TNO.Logging.Common.IdFactories;

/// <summary>
/// Represents a thread safe id factory.
/// </summary>
public class SafeIdFactory
{
   #region Fields
   private readonly object _lock = new object();
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
      lock (_lock)
      {
         ulong id = _nextId;
         _nextId++;

         return id;
      }
   }
   #endregion
}