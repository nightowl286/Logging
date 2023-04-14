using System.Diagnostics;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Writing.Serialisers.LogData.Tables;

namespace TNO.ReadingWriting.Tests.LogData;

[TestClass]
public class TableInfoReadWriteTests : ReadWriteTestsBase<TableInfoSerialiser, TableInfoDeserialiserLatest, ITableInfo>
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

   #region Methods
   protected override ITableInfo CreateData()
   {
      // Arrange
      Dictionary<uint, object?> table = new Dictionary<uint, object?>(ValidValues.Length);
      uint id = 0;
      foreach (object? value in ValidValues)
         table.Add(id++, value);

      TableInfo tableInfo = new TableInfo(table);

      // Act + Assert
      return tableInfo;
   }
   protected override void Verify(ITableInfo expected, ITableInfo result)
   {
      Assert.That.AreEqual(expected.Table.Count, result.Table.Count);

      foreach (KeyValuePair<uint, object?> expectedPair in expected.Table)
      {
         object? expectedValue = expectedPair.Value;
         object? resultValue = result.Table[expectedPair.Key];

         Debug.WriteLine($"Checking types: <{expectedValue?.GetType()}> <{resultValue?.GetType()}>");

         Assert.That.AreEqual(expectedValue, resultValue);
      }
   }
   #endregion
}