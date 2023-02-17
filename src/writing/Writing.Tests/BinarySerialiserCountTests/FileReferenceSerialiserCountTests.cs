using TNO.Logging.Common.Abstractions;
using TNO.Logging.Writing.Serialisers;

namespace TNO.Writing.Tests.BinarySerialiserCountTests;

[TestClass]
public class FileReferenceSerialiserCountTests : BinarySerialiserCountTestBase<FileReferenceSerialiser, FileReference>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      FileReference reference = new FileReference("file", 5);

      // Act + Verify
      CountTestBase(reference);
   }
   #endregion
}
