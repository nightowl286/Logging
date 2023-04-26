using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData;

[TestClass]
public class FileReferenceSerialiserCountTests : BinarySerialiserCountTestBase<FileReferenceSerialiser, FileReference>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      FileReference reference = new FileReference("file", 5);

      // Act + Assert
      CountTestBase(reference);
   }
   #endregion
}
