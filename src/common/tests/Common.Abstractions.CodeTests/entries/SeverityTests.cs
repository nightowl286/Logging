using TNO.Logging.Common.Abstractions.Entries;

namespace TNO.Common.Abstractions.CodeTests.entries;

[TestClass]
[TestCategory(Category.Severity)]
public sealed class SeverityTests : ImportanceTestsBase
{
   #region Tests
   [TestMethod]
   public void PropertiesWithReturnType_NoUnexpectedNames()
   {
      // Arrange
      Type helperType = typeof(Severity);
      IEnumerable<Importance> values = Severity.GetAll();

      // Act
      PropertiesWithReturnType_NoUnexpectedNames(helperType, values);
   }

   [TestMethod]
   public void PropertiesWithExpectedName_HaveExpectedReturnTypeAndValue()
   {
      // Arrange
      Type helperType = typeof(Severity);
      IEnumerable<Importance> values = Severity.GetAll();

      // Act
      PropertiesWithExpectedName_HaveExpectedReturnTypeAndValue(helperType, values);
   }
   #endregion
}
