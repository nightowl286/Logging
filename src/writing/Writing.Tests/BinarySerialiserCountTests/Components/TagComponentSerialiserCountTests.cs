using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class TagComponentSerialiserCountTests : BinarySerialiserCountTestBase<TagComponentSerialiser, ITagComponent>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      TagComponent component = new TagComponent(5);

      // Act + Assert
      CountTestBase(component);
   }
   #endregion
}