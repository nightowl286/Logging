using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.Entries.Components;

namespace TNO.Logging.Reading.Entries.Components.Table;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="ITableComponent"/>.
/// </summary>
internal static class TableComponentFactory
{
   #region Functions
   public static ITableComponent Version0(ITableInfo tableInfo) => new TableComponent(tableInfo);
   #endregion
}
