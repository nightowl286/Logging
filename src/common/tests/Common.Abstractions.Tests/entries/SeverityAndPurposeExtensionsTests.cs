using TNO.Logging.Common.Abstractions.Entries;

namespace Common.Abstractions.Tests.entries;

[TestClass]
[TestCategory(Category.Severity)]
[TestCategory(Category.Purpose)]
public class SeverityAndPurposeExtensionsTests
{
   #region Tests
   [DataRow(SeverityAndPurpose.Empty)]
   [DataRow(SeverityAndPurpose.None)]
   [DataRow(SeverityAndPurpose.NoPurpose)]
   [DataRow(SeverityAndPurpose.NoSeverity)]
   [TestMethod]
   public void IsNone_WithValueEquivalentToNone_ReturnsTrue(SeverityAndPurpose value)
   {
      // Act
      bool result = SeverityAndPurposeExtensions.IsNone(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(SeverityAndPurpose.Negligible)]
   [DataRow(SeverityAndPurpose.InheritSeverity)]
   [DataRow(SeverityAndPurpose.Telemetry)]
   [DataRow(SeverityAndPurpose.InheritPurpose)]
   [TestMethod]
   public void IsNone_WithNotNoneValue_ReturnsFalse(SeverityAndPurpose value)
   {
      // Act
      bool result = SeverityAndPurposeExtensions.IsNone(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion
}
