using TNO.Logging.Common.Abstractions.LogData.Tables;

namespace TNO.Logging.Common.LogData.Tables;

/// <summary>
/// Represents info about a table.
/// </summary>
public record class TableInfo(IReadOnlyDictionary<uint, object?> Table) : ITableInfo;