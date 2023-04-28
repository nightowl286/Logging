using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Reading.LogData.General;
using TNO.Logging.Writing.Serialisers.LogData.General;

namespace TNO.ReadingWriting.Tests.LogData.General;

[TestClass]
public class TableInfoReadWriteTests : BinaryReadWriteTestsBase<TableInfoSerialiser, TableInfoDeserialiser, ITableInfo>
{
   #region Methods
   protected override void Setup(out TableInfoSerialiser writer, out TableInfoDeserialiser reader)
   {
      writer = (TableInfoSerialiser)GeneralSerialiser.Instance.Get<ITableInfo>();
      reader = (TableInfoDeserialiser)GeneralDeserialiser.Instance.Get<ITableInfo>();
   }

   protected override IEnumerable<Annotated<ITableInfo>> CreateData()
   {
      foreach (Annotated value in ValidPrimitiveValues.Values)
         yield return new(CreateTable(value.Data), value.Annotation);

      yield return new(CreateTable(ValidPrimitiveValues.Values.Select(d => d.Data).ToArray()), "all primitives");
   }
   protected override void Verify(ITableInfo expected, ITableInfo result)
   {
      IReadOnlyDictionary<uint, object?> expectedTable = expected.Table;
      IReadOnlyDictionary<uint, object?> resultTable = result.Table;

      Assert.That.AreEqual(expectedTable.Count, resultTable.Count);
      foreach (KeyValuePair<uint, object?> pair in expectedTable)
      {
         object? resultValue = resultTable[pair.Key];
         Assert.That.AreEqual(pair.Value, resultValue);
      }
   }
   #endregion

   #region Helpers
   private static ITableInfo CreateTable(object? value) => CreateTable(new object?[] { value });
   private static ITableInfo CreateTable(object?[] values)
   {
      Dictionary<uint, object?> table = new Dictionary<uint, object?>();
      for (int i = 0; i < values.Length; i++)
         table.Add((uint)i, values[i]);

      TableInfo info = new TableInfo(table);
      return info;
   }
   #endregion
}
