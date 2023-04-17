using TNO.Logging.Common.Abstractions.Entries.Importance;

namespace Common.Abstractions.Tests.entries;

[TestClass]
[TestCategory(Category.Severity)]
[TestCategory(Category.Extensions)]
public class SeverityExtensionsTests
{
   #region Methods
   #region Is Severity Set
   [DataRow(ImportanceCombination.NoSeverity, DisplayName = "No Severity")]
   [DataRow(ImportanceCombination.Negligible, DisplayName = "Severity")]
   [DataRow(ImportanceCombination.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(ImportanceCombination.Negligible | ImportanceCombination.Telemetry, DisplayName = "Combined")]
   [DataRow(ImportanceCombination.Inherit, DisplayName = "Inherit")]
   [DataRow(ImportanceCombination.None, DisplayName = "None")]
   [TestMethod("Is Severity Set | With Severity")]
   public void IsSeveritySet_WithSeverity_ReturnsTrue(ImportanceCombination value)
   {
      // Act
      bool result = SeverityExtensions.IsSeveritySet(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(ImportanceCombination.Empty, DisplayName = "Empty")]
   [DataRow(ImportanceCombination.Telemetry, DisplayName = "Purpose Only")]
   [DataRow(ImportanceCombination.NoPurpose, DisplayName = "No Purpose Only")]
   [DataRow(ImportanceCombination.InheritPurpose, DisplayName = "Inherit Purpose Only")]
   [TestMethod("Is Severity Set | Without Severity")]
   public void IsSeveritySet_WithoutSeverity_ReturnsFalse(ImportanceCombination value)
   {
      // Act
      bool result = SeverityExtensions.IsSeveritySet(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion

   #region Get Set Severity
   [DataRow(ImportanceCombination.Negligible, ImportanceCombination.Negligible, DisplayName = "Severity -> Severity")]
   [DataRow(ImportanceCombination.InheritSeverity, ImportanceCombination.InheritSeverity, DisplayName = "Inherit Severity -> Inherit Severity")]
   [DataRow(ImportanceCombination.NoSeverity, ImportanceCombination.NoSeverity, DisplayName = "No Severity -> No Severity")]
   [DataRow(ImportanceCombination.Negligible | ImportanceCombination.Telemetry, ImportanceCombination.Negligible, DisplayName = "Combined -> Severity")]
   [DataRow(ImportanceCombination.Inherit, ImportanceCombination.InheritSeverity, DisplayName = "Inherit -> Inherit Severity")]
   [DataRow(ImportanceCombination.None, ImportanceCombination.NoSeverity, DisplayName = "None -> No Severity")]
   [TestMethod("Get Set Severity | With Severity")]
   public void GetSetSeverity_WithSeverity_ReturnsExpectedSeverity(ImportanceCombination value, ImportanceCombination expected)
   {
      // Act
      ImportanceCombination result = SeverityExtensions.GetSetSeverity(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }

   [DataRow(ImportanceCombination.Empty, DisplayName = "Empty")]
   [DataRow(ImportanceCombination.Telemetry, DisplayName = "Purpose")]
   [DataRow(ImportanceCombination.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(ImportanceCombination.NoPurpose, DisplayName = "No Purpose")]
   [TestMethod("Get Set Severity | Without Severity")]
   public void GetSetSeverity_WithoutSeverity_ReturnsEmpty(ImportanceCombination value)
   {
      // Arrange
      ImportanceCombination expected = ImportanceCombination.Empty;

      // Act
      ImportanceCombination result = SeverityExtensions.GetSetSeverity(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }
   #endregion
   #region Get Severity
   [DataRow(ImportanceCombination.Negligible, ImportanceCombination.Negligible, DisplayName = "Severity -> Severity")]
   [DataRow(ImportanceCombination.InheritSeverity, ImportanceCombination.InheritSeverity, DisplayName = "Inherit Severity -> Inherit Severity")]
   [DataRow(ImportanceCombination.NoSeverity, ImportanceCombination.NoSeverity, DisplayName = "No Severity -> No Severity")]
   [DataRow(ImportanceCombination.Negligible | ImportanceCombination.Telemetry, ImportanceCombination.Negligible, DisplayName = "Combined -> Severity")]
   [DataRow(ImportanceCombination.Inherit, ImportanceCombination.InheritSeverity, DisplayName = "Inherit -> Inherit Severity")]
   [DataRow(ImportanceCombination.None, ImportanceCombination.NoSeverity, DisplayName = "None -> No Severity")]
   [TestMethod("Get Severity | With Severity")]
   public void GetSeverity_WithSeverity_ReturnsExpectedSeverity(ImportanceCombination value, ImportanceCombination expected)
   {
      // Act
      ImportanceCombination result = SeverityExtensions.GetSeverity(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }

   [DataRow(ImportanceCombination.Empty, DisplayName = "Empty")]
   [DataRow(ImportanceCombination.Telemetry, DisplayName = "Purpose")]
   [DataRow(ImportanceCombination.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(ImportanceCombination.NoPurpose, DisplayName = "No Purpose")]
   [TestMethod("Get Severity | Without Severity")]
   public void GetSeverity_WithoutSeverity_ReturnsNoSeverity(ImportanceCombination value)
   {
      // Arrange
      ImportanceCombination expected = ImportanceCombination.NoSeverity;

      // Act
      ImportanceCombination result = SeverityExtensions.GetSeverity(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }
   #endregion

   #region Has Severity
   [DataRow(ImportanceCombination.Negligible, DisplayName = "Severity")]
   [DataRow(ImportanceCombination.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(ImportanceCombination.Negligible | ImportanceCombination.Telemetry, DisplayName = "Combined")]
   [DataRow(ImportanceCombination.Inherit, DisplayName = "Inherit")]
   [TestMethod("Has Severity | With Severity")]
   public void HasSeverity_WithSeverity_ReturnsTrue(ImportanceCombination value)
   {
      // Act
      bool result = SeverityExtensions.HasSeverity(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(ImportanceCombination.Empty, DisplayName = "Empty")]
   [DataRow(ImportanceCombination.NoSeverity, DisplayName = "No Severity")]
   [DataRow(ImportanceCombination.Telemetry, DisplayName = "Purpose")]
   [DataRow(ImportanceCombination.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(ImportanceCombination.NoPurpose, DisplayName = "No Purpose")]
   [DataRow(ImportanceCombination.None, DisplayName = "None")]
   [TestMethod("Has Severity | Without Severity")]
   public void HasSeverity_WithoutSeverity_ReturnsFalse(ImportanceCombination value)
   {
      // Act
      bool result = SeverityExtensions.HasSeverity(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion
   #endregion
}