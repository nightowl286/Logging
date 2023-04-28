using System.Collections;
using System.Diagnostics.CodeAnalysis;
using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;

namespace TNO.Logging.Helpers.General;

/// <summary>
/// Contains useful functions related to the <see cref="ICollectionInfo"/>.
/// </summary>
public static class CollectionInfoHelper
{
   #region Functions
   /// <summary>
   /// Tries to convert the given <paramref name="value"/> into an <see cref="ICollectionInfo"/>.
   /// </summary>
   /// <param name="writeContext">The write context to use to generate the needed ids.</param>
   /// <param name="dataCollector">The data collector to deposit new log data to.</param>
   /// <param name="value">The value to try and convert.</param>
   /// <param name="converted">The converted <paramref name="value"/>.</param>
   /// <returns>
   /// <see langword="true"/> if the conversion was successful, <see langword="false"/> otherwise.
   /// </returns>
   public static bool TryConvert(ILogWriteContext writeContext, ILogDataCollector dataCollector, object value, [NotNullWhen(true)] out ICollectionInfo? converted)
   {
      if (IsCollection(value.GetType()))
      {
         converted = Convert(writeContext, dataCollector, (IEnumerable)value);
         return true;
      }

      converted = null;
      return false;
   }
   #endregion

   #region Helpers
   private static ICollectionInfo Convert(ILogWriteContext writeContext, ILogDataCollector dataCollector, IEnumerable enumerable)
   {
      List<object?> values = new List<object?>();
      foreach (object? value in enumerable)
      {
         object? convertedValue = PrimitiveValueHelper.Convert(writeContext, dataCollector, value);
         values.Add(convertedValue);
      }

      return new CollectionInfo(values);
   }
   private static bool IsCollection(Type type)
   {
      if (type.ImplementsOpenInterface(typeof(ICollection<>)))
         return true;

      if (type.ImplementsOpenInterface(typeof(IReadOnlyCollection<>)))
         return true;

      return type.FindInterfaces(IsInterface, typeof(ICollection)).Length > 0;
   }
   private static bool IsInterface(Type type, object? targetInterface)
   {
      if (targetInterface is not Type typedInterface)
         return false;

      return type == typedInterface;
   }
   #endregion
}
