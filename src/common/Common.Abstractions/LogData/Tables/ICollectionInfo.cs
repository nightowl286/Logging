using System.Collections.Generic;

namespace TNO.Logging.Common.Abstractions.LogData.Tables;

/// <summary>
/// Denotes info about a collection.
/// </summary>
public interface ICollectionInfo
{
   #region Properties
   /// <summary>The collection that contains the stored data.</summary>
   IReadOnlyCollection<object?> Collection { get; }
   #endregion
}
