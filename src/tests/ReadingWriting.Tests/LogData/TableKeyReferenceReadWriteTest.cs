using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.ReadingWriting.Tests.LogData;

[TestClass]
public class TableKeyReferenceReadWriteTest : ReadWriteTestBase<TableKeyReferenceSerialiser, TableKeyReferenceDeserialiserLatest, TableKeyReference>
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