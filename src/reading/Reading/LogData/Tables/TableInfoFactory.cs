﻿using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Reading.Abstractions.LogData.Tables;

namespace TNO.Logging.Reading.LogData.Tables;

/// <summary>
/// A factory class that should be used in instances of the <see cref="ITableInfoDeserialiser"/>.
/// </summary>
internal static class TableInfoFactory
{
   #region Functions
   public static ITableInfo Version0(IReadOnlyDictionary<uint, object?> table) => new TableInfo(table);
   #endregion
}