using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData;

[TestClass]
public class TagReferenceSerialiserCountTests : BinarySerialiserCountTestBase<TagReferenceSerialiser, TagReference>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      TagReference reference = new TagReference("tag", 5);

      // Act + Assert
      CountTestBase(reference);
   }
   #endregion
}
