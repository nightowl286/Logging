using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData;

[TestClass]
public class TableKeyReferenceSerialiserCountTests : BinarySerialiserCountTestBase<TableKeyReferenceSerialiser, TableKeyReference>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      TableKeyReference reference = new TableKeyReference("key", 5);

      // Act + Assert
      CountTestBase(reference);
   }
   #endregion
}
