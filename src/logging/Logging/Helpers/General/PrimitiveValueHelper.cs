using System.Diagnostics.CodeAnalysis;
using TNO.Logging.Common.Abstractions.LogData.General.Primitives;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;

namespace TNO.Logging.Logging.Helpers.General;

/// <summary>
/// Contains useful functions related to primitive values.
/// </summary>
public static class PrimitiveValueHelper
{
   #region Functions
   /// <summary>Converts the given <paramref name="value"/> into a known primitive value.</summary>
   /// <param name="writeContext">The write context to use to generate the needed ids.</param>
   /// <param name="dataCollector">The data collector to deposit new log data to.</param>
   /// <param name="value">The value to convert.</param>
   /// <returns>The converted <paramref name="value"/>.</returns>
   [return: NotNullIfNotNull(nameof(value))]
   public static object? Convert(ILogWriteContext writeContext, ILogDataCollector dataCollector, object? value)
   {
      if (value is null)
         return null;

      Type type = value.GetType();
      if (type.TryGetPrimitiveKind(out PrimitiveKind? _, out _))
         return value;

      // Note(Nightowl): Table needs to be resolved first, as every table will also be a collection;
      if (TableInfoHelper.TryConvert(writeContext, dataCollector, value, out ITableInfo? convertedTable))
         return convertedTable;

      if (CollectionInfoHelper.TryConvert(writeContext, dataCollector, value, out ICollectionInfo? convertedCollection))
         return convertedCollection;

      ulong valueTypeId = TypeInfoHelper.EnsureIdsForAssociatedTypes(writeContext, dataCollector, type);
      return new UnknownPrimitive(valueTypeId);
   }
   #endregion
}
