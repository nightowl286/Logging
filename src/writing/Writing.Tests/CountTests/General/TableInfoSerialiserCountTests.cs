using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Writing.Serialisers.LogData.General;
using TNO.Writing.Tests.BinarySerialiserCountTests;

namespace TNO.Writing.Tests.CountTests.General;

[TestClass]
public class TableInfoSerialiserCountTests : BinarySerialiserCountTestBase<TableInfoSerialiser, ITableInfo>
{
   #region Methods
   protected override TableInfoSerialiser Setup() => (TableInfoSerialiser)GeneralSerialiser.Instance.Get<ITableInfo>();

   [DynamicData(nameof(ValidPrimitiveValues.AsArguments), typeof(ValidPrimitiveValues), DynamicDataSourceType.Method)]
   [TestMethod]
   public void Count_WithOne(object? value)
   {
      // Arrange
      Dictionary<uint, object?> table = new Dictionary<uint, object?>()
      {
         {1, value }
      };

      TableInfo tableInfo = new TableInfo(table);

      // Act + Assert
      CountTestBase(tableInfo);
   }

   [TestMethod]
   public void Count_WithAll()
   {
      // Arrange
      Dictionary<uint, object?> table = new Dictionary<uint, object?>();

      object?[] values = ValidPrimitiveValues.Values;
      for (int i = 0; i < values.Length; i++)
         table.Add((uint)i, values[i]);

      TableInfo tableInfo = new TableInfo(table);

      // Act + Assert
      CountTestBase(tableInfo);
   }
   #endregion
}
