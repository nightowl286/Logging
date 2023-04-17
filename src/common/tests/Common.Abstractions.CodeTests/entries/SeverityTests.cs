using TNO.Logging.Common.Abstractions.Entries.Importance;

namespace TNO.Common.Abstractions.CodeTests.entries;

[TestClass]
[TestCategory(Category.Severity)]
public sealed class SeverityTests : ImportanceTestsBase<Severity>
{
   #region Tests
   [TestMethod]
   public void PropertiesWithReturnType_NoUnexpectedNames()
   {
      // Arrange
      IEnumerable<ImportanceCombination> values = Severity.GetAll();

      // Act
      PropertiesWithReturnType_NoUnexpectedNames(values);
   }

   [TestMethod]
   public void PropertiesWithExpectedName_HaveExpectedReturnTypeAndValue()
   {
      // Arrange
      IEnumerable<ImportanceCombination> values = Severity.GetAll();

      // Act
      PropertiesWithExpectedName_HaveExpectedReturnTypeAndValue(values);
   }

   protected override ImportanceCombination GetImportanceValue(Severity component) => component.Value;
   #endregion
}