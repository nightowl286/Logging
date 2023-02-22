namespace TNO.Logging.Common.Abstractions.LogData;

/// <summary>
/// Represents a link between a <see cref="Tag"/> and it's <see cref="Id"/>.
/// </summary>
public readonly struct TagReference
{
   #region Fields
   /// <summary>The tag that the <see cref="Id"/> is associated with.</summary>
   public readonly string Tag;

   /// <summary>The id that the <see cref="Tag"/> is associated with.</summary>
   public readonly ulong Id;
   #endregion

   #region Constructors
   /// <summary>Creates a new tag reference.</summary>
   /// <param name="tag">The tag.</param>
   /// <param name="id">The id of the <paramref name="tag"/>.</param>
   public TagReference(string tag, ulong id)
   {
      Tag = tag;
      Id = id;
   }
   #endregion
}
