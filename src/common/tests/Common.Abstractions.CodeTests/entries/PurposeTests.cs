using TNO.Logging.Common.Abstractions.Entries;

namespace TNO.Common.Abstractions.CodeTests.entries;

[TestClass]
[TestCategory(Category.Purpose)]
public sealed class PurposeTests : ImportanceTestsBase
{
   #region Tests
   [TestMethod]
   public void PropertiesWithReturnType_NoUnexpectedNames()
   {
      // Arrange
      Type helperType = typeof(Purpose);
      IEnumerable<Importance> values = Purpose.GetAll();

      // Act
      PropertiesWithReturnType_NoUnexpectedNames(helperType, values);
   }

   [TestMethod]
   public void PropertiesWithExpectedName_HaveExpectedReturnTypeAndValue()
   {
      // Arrange
      Type helperType = typeof(Purpose);
      IEnumerable<Importance> values = Purpose.GetAll();

      // Act
      PropertiesWithExpectedName_HaveExpectedReturnTypeAndValue(helperType, values);
   }
   #endregion
}

