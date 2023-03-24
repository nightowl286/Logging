using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class SimpleStackTraceComponentSerialiserCountTests : BinarySerialiserCountTestBase<SimpleStackTraceComponentSerialiser, ISimpleStackTraceComponent>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      SimpleStackTraceComponent component = new SimpleStackTraceComponent("some stack trace", 1);

      // Act + Assert
      CountTestBase(component);
   }
   #endregion
}