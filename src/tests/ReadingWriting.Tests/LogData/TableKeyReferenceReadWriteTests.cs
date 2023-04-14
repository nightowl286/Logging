using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.ReadingWriting.Tests.LogData;

[TestClass]
public class TableKeyReferenceReadWriteTests : ReadWriteTestsBase<TableKeyReferenceSerialiser, TableKeyReferenceDeserialiserLatest, TableKeyReference>
{
   #region Methods
   protected override TableKeyReference CreateData()
   {
      return new TableKeyReference("key", 5);
   }

   protected override void Verify(TableKeyReference expected, TableKeyReference result)
   {
      Assert.That.AreEqual(expected.Key, result.Key);
      Assert.That.AreEqual(expected.Id, result.Id);
   }
   #endregion
}