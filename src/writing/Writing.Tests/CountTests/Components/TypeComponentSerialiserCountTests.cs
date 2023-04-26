using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class TypeComponentSerialiserCountTests : BinarySerialiserCountTestBase<TypeComponentSerialiser, ITypeComponent>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      TypeComponent component = new TypeComponent(5);

      // Act + Assert
      CountTestBase(component);
   }
   #endregion
}