using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Primitives;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Table"/> component.
/// </summary>
public class TableComponent : ITableComponent
{
   #region Properties
   /// <inheritdoc/>
   public ITableInfo Table { get; }

   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Table;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TableComponent"/>.</summary>
   /// <param name="table">The table information.</param>
   public TableComponent(ITableInfo table)
   {
      Table = table;
   }
   #endregion
}
