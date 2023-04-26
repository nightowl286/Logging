using TNO.Logging.Common.Abstractions.LogData.Primitives;

namespace TNO.Logging.Common.LogData.Tables;

/// <summary>
/// Represents info about a table.
/// </summary>
public class TableInfo : ITableInfo
{
   #region Properties
   /// <inheritdoc/>
   public IReadOnlyDictionary<uint, object?> Table { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TableInfo"/>.</summary>
   /// <param name="table">The table that contains the stored data.</param>
   public TableInfo(IReadOnlyDictionary<uint, object?> table)
   {
      Table = table;
   }
   #endregion
}