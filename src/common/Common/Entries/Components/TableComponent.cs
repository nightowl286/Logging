using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Primitives;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Table"/> component.
/// </summary>
public record class TableComponent(ITableInfo Table) : ITableComponent
{
   #region Properties
   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Table;
   #endregion
}
