using TNO.Logging.Common.Abstractions.Entries.Importance;

namespace TNO.Common.Abstractions.CodeTests.entries;

[TestClass]
[TestCategory(Category.Purpose)]
public sealed class PurposeTests : ImportanceTestsBase<Purpose>
{
   #region Tests
   [TestMethod]
   public void PropertiesWithReturnType_NoUnexpectedNames()
   {
      // Arrange
      IEnumerable<ImportanceCombination> values = Purpose.GetAll();

      // Act
      PropertiesWithReturnType_NoUnexpectedNames(values);
   }

   [TestMethod]
   public void PropertiesWithExpectedName_HaveExpectedReturnTypeAndValue()
   {
      // Arrange
      IEnumerable<ImportanceCombination> values = Purpose.GetAll();

      // Act
      PropertiesWithExpectedName_HaveExpectedReturnTypeAndValue(values);
   }
   #endregion

   #region Helpers
   protected override ImportanceCombination GetImportanceValue(Purpose component) => component.Value;
   #endregion
}