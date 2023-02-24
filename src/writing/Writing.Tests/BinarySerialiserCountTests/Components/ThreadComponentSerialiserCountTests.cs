using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class ThreadComponentSerialiserCountTests : BinarySerialiserCountTestBase<ThreadComponentSerialiser, IThreadComponent>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      ThreadComponent component = new ThreadComponent(
         1,
         "name",
         ThreadState.Stopped,
         true,
         ThreadPriority.Normal,
         ApartmentState.MTA);

      // Act + Assert
      CountTestBase(component);
   }
   #endregion
}
