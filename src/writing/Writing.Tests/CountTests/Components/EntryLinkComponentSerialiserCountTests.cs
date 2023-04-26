using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class EntryLinkComponentSerialiserCountTests : BinarySerialiserCountTestBase<EntryLinkComponentSerialiser, IEntryLinkComponent>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      EntryLinkComponent component = new EntryLinkComponent(5);

      // Act + Assert
      CountTestBase(component);
   }
   #endregion
}