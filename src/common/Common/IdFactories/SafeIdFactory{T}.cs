namespace TNO.Logging.Common.IdFactories;

/// <summary>
/// Represents a thread safe id factory where each id is associated with value of the type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the values that the ids will be associated with.</typeparam>
public class SafeIdFactory<T> where T : notnull
{
   #region Fields
   private readonly Dictionary<T, ulong> _idCache = new Dictionary<T, ulong>();
   private ulong _nextId;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="SafeIdFactory{T}"/>.</summary>
   /// <param name="startId">The first id that will be given out.</param>
   public SafeIdFactory(ulong startId)
   {
      _nextId = startId;
   }
   #endregion

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
      lock (_idCache)
      {
         if (_idCache.TryGetValue(data, out id))
            return false;

         id = _nextId;
         _nextId++;

         _idCache.Add(data, id);

         return true;
      }
   }
   #endregion
}