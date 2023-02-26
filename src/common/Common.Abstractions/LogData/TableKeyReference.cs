using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Abstractions.LogData;

/// <summary>
/// Represents a link between a key in a <see cref="ITableComponent"/> and it's <see cref="Id"/>.
/// </summary>
public readonly struct TableKeyReference
{
   #region Fields
   /// <summary>The key that the <see cref="Id"/> is associated with.</summary>
   public readonly string Key;

   /// <summary>The id that the <see cref="Key"/> is associated with.</summary>
   public readonly uint Id;
   #endregion

   #region Constructors
   /// <summary>Creates a new table key reference.</summary>
   /// <param name="key">The key in the table.</param>
   /// <param name="id">The id of the <paramref name="key"/>.</param>
   public TableKeyReference(string key, uint id)
   {
      Key = key;
      Id = id;
   }
   #endregion
}