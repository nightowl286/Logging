using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Table;

namespace TNO.Logging.Reading.Entries.Components.Table;

/// <summary>
/// A factory class that should be used in instances of the <see cref="ITableComponentDeserialiser"/>.
/// </summary>
internal static class TableComponentFactory
{
   #region Functions
   public static ITableComponent Version0(ITableInfo tableInfo) => new TableComponent(tableInfo);
   #endregion
}
