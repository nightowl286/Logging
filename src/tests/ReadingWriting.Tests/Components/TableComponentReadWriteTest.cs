using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class TableComponentReadWriteTest : ReadWriteTestBase<TableComponentSerialiser, TableComponentDeserialiserLatest, ITableComponent>
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

   #region Methods
   protected override ITableComponent CreateData()
   {
      // Arrange
      Dictionary<uint, object> table = new Dictionary<uint, object>(ValidValues.Length);
      uint id = 0;
      foreach (object value in ValidValues)
         table.Add(id++, value);

      TableComponent component = new TableComponent(table);

      // Act + Assert
      return component;
   }
   protected override void Verify(ITableComponent expected, ITableComponent result)
   {
      Assert.That.AreEqual(expected.Table.Count, result.Table.Count);

      foreach (KeyValuePair<uint, object> expectedPair in expected.Table)
      {
         object expectedValue = expectedPair.Value;
         object resultValue = result.Table[expectedPair.Key];

         Assert.That.AreEqual(expectedValue, resultValue);
      }
   }
   #endregion
}