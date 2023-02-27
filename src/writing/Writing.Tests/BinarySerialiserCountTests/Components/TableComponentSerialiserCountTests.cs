using System.Reflection;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class TableComponentSerialiserCountTests : BinarySerialiserCountTestBase<TableComponentSerialiser, ITableComponent>
{
   #region Properties
   private static object[] ValidValues => new object[]
   {
      (byte)1, (sbyte)2, (ushort)3, (short)4, 5u, 6, 7uL, 8L, // integer
      8.0f, 9.0d, 10.0m, // floating
      't', (char)0x7FFu, (char)0xFFFFu, // chars
      "test",
      true, false, // bool
      TimeSpan.FromHours(3), DateTime.Now, DateTimeOffset.Now, // time
      TimeZoneInfo.Utc, TimeZoneInfo.Local // time zone
   };
   #endregion

   #region Tests
   [TestMethod]
   public void Count_EmptyTable()
   {
      // Arrange
      Dictionary<uint, object> table = new Dictionary<uint, object>();
      TableComponent component = new TableComponent(table);

      // Act + Assert
      CountTestBase(component);
   }

   [DynamicData(nameof(GetValidValuesData), DynamicDataSourceType.Method,
      DynamicDataDisplayName = nameof(GetValidValuesTestMethodName))]
   [TestMethod]
   public void Count_SingleEntry(object value)
   {
      // Arrange
      Dictionary<uint, object> table = new Dictionary<uint, object>()
      {
         {0, value }
      };
      TableComponent component = new TableComponent(table);

      // Act + Assert
      CountTestBase(component);
   }

   [TestMethod]
   public void Count_AllDataKindEntries()
   {
      // Arrange
      Dictionary<uint, object> table = new Dictionary<uint, object>(ValidValues.Length);
      uint id = 0;
      foreach (object value in ValidValues)
         table.Add(id++, value);

      TableComponent component = new TableComponent(table);

      // Act + Assert
      CountTestBase(component);
   }
   #endregion

   #region Helpers
   public static IEnumerable<object[]> GetValidValuesData()
   {
      foreach (object value in ValidValues)
      {
         yield return new object[] { value };
      }
   }
   public static string GetValidValuesTestMethodName(MethodInfo methodInfo, object[] values)
   {
      object val = values[0];

      return $"{methodInfo.Name}({val.GetType().Name}) : {val}";
   }
   #endregion
}
