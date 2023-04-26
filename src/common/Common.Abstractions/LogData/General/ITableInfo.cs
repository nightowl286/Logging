using System.Collections.Generic;

namespace TNO.Logging.Common.Abstractions.LogData.Primitives;

/// <summary>
/// Denotes info about a table.
/// </summary>
public interface ITableInfo
{
   #region Properties
   /// <summary>The table that contains the stored data.</summary>
   IReadOnlyDictionary<uint, object?> Table { get; }
   #endregion
}
