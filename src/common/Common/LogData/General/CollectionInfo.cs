using TNO.Logging.Common.Abstractions.LogData.Primitives;

namespace TNO.Logging.Common.LogData.Tables;

/// <summary>
/// Represents info about a table.
/// </summary>
public class CollectionInfo : ICollectionInfo
{
   #region Properties
   /// <inheritdoc/>
   public IReadOnlyCollection<object?> Collection { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="CollectionInfo"/>.</summary>
   /// <param name="collection">The collection that contains the stored data.</param>
   public CollectionInfo(IReadOnlyCollection<object?> collection)
   {
      Collection = collection;
   }
   #endregion
}