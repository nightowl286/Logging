using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Writing.Serialisers.LogData.Tables;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData.Tables;

[TestClass]
[TestCategory(Category.Components)]
public class TableInfoSerialiserCountTests : BinarySerialiserCountTestBase<TableInfoSerialiser, ITableInfo>
{
   #region Properties
   private static object?[] ValidValues => new object?[]
   {
      (byte)1, (sbyte)2, (ushort)3, (short)4, 5u, 6, 7uL, 8L, // integer
      8.0f, 9.0d, 10.0m, // floating
      't', (char)0x7FFu, (char)0xFFFFu, // chars
      "test",
      true, false, // bool
      TimeSpan.FromHours(3), DateTime.Now, DateTimeOffset.Now, // time
      TimeZoneInfo.Utc, TimeZoneInfo.Local, // time zone
      null,
      new UnknownTableValue(1),
   };
   #endregion

   #region Tests
   [TestMethod]
   public void Count_EmptyTable()
   {
      // Arrange
      Dictionary<uint, object?> table = new Dictionary<uint, object?>();
      TableInfo tableInfo = new TableInfo(table);

      // Act + Assert
      CountTestBase(tableInfo);
   }

   [DynamicData(nameof(GetValidValuesData), DynamicDataSourceType.Method,
      DynamicDataDisplayName = nameof(GetValidValuesTestMethodName))]
   [TestMethod]
   public void Count_SingleEntry(object? value)
   {
      // Arrange
      Dictionary<uint, object?> table = new Dictionary<uint, object?>()
      {
         {0, value }
      };
      TableInfo tableInfo = new TableInfo(table);

      // Act + Assert
      CountTestBase(tableInfo);
   }

   [TestMethod]
   public void Count_AllDataKindEntries()
   {
      // Arrange
      Dictionary<uint, object?> table = new Dictionary<uint, object?>(ValidValues.Length);
      uint id = 0;
      foreach (object? value in ValidValues)
         table.Add(id++, value);

      TableInfo tableInfo = new TableInfo(table);

      // Act + Assert
      CountTestBase(tableInfo);
   }
   #endregion

   #region Helpers
   public static IEnumerable<object?[]> GetValidValuesData()
   {
      foreach (object? value in ValidValues)
      {
         yield return new object?[] { value };
      }
   }
   public static string GetValidValuesTestMethodName(MethodInfo methodInfo, object?[] values)
   {
      object? val = values[0];

      return $"{methodInfo.Name}({val?.GetType().Name}) : {val ?? "<null>"}";
   }
   #endregion
}
