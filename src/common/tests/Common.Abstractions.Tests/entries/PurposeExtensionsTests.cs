using TNO.Logging.Common.Abstractions.Entries.Importance;

namespace Common.Abstractions.Tests.entries;

[TestClass]
[TestCategory(Category.Purpose)]
[TestCategory(Category.Extensions)]
public class PurposeExtensionsTests
{
   #region Tests
   #region Is Purpose Set
   [DataRow(ImportanceCombination.NoPurpose, DisplayName = "No Purpose")]
   [DataRow(ImportanceCombination.Telemetry, DisplayName = "Purpose")]
   [DataRow(ImportanceCombination.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(ImportanceCombination.Negligible | ImportanceCombination.Telemetry, DisplayName = "Combined")]
   [DataRow(ImportanceCombination.Inherit, DisplayName = "Inherit")]
   [DataRow(ImportanceCombination.None, DisplayName = "None")]
   [TestMethod("Is Purpose Set | With Purpose")]
   public void IsPurposeSet_WithPurpose_ReturnsTrue(ImportanceCombination value)
   {
      // Act
      bool result = PurposeExtensions.IsPurposeSet(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(ImportanceCombination.Empty, DisplayName = "Empty")]
   [DataRow(ImportanceCombination.Negligible, DisplayName = "Severity Only")]
   [DataRow(ImportanceCombination.NoSeverity, DisplayName = "No Severity Only")]
   [DataRow(ImportanceCombination.InheritSeverity, DisplayName = "Inherit Severity Only")]
   [TestMethod("Is Purpose Set | Without Purpose")]
   public void IsPurposeSet_WithoutPurpose_ReturnsFalse(ImportanceCombination value)
   {
      // Act
      bool result = PurposeExtensions.IsPurposeSet(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion

   #region Get Set Purpose
   [DataRow(ImportanceCombination.Telemetry, ImportanceCombination.Telemetry, DisplayName = "Purpose -> Purpose")]
   [DataRow(ImportanceCombination.InheritPurpose, ImportanceCombination.InheritPurpose, DisplayName = "Inherit Purpose -> Inherit Purpose")]
   [DataRow(ImportanceCombination.NoPurpose, ImportanceCombination.NoPurpose, DisplayName = "No Purpose -> No Purpose")]
   [DataRow(ImportanceCombination.Negligible | ImportanceCombination.Telemetry, ImportanceCombination.Telemetry, DisplayName = "Combined -> Purpose")]
   [DataRow(ImportanceCombination.Inherit, ImportanceCombination.InheritPurpose, DisplayName = "Inherit -> Inherit Purpose")]
   [DataRow(ImportanceCombination.None, ImportanceCombination.NoPurpose, DisplayName = "None -> No Purpose")]
   [TestMethod("Get Set Purpose | With Purpose")]
   public void GetSetPurpose_WithPurpose_ReturnsExpectedPurpose(ImportanceCombination value, ImportanceCombination expected)
   {
      // Act
      ImportanceCombination result = PurposeExtensions.GetSetPurpose(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }

   [DataRow(ImportanceCombination.Empty, DisplayName = "Empty")]
   [DataRow(ImportanceCombination.Negligible, DisplayName = "Severity")]
   [DataRow(ImportanceCombination.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(ImportanceCombination.NoSeverity, DisplayName = "No Severity")]
   [TestMethod("Get Set Purpose | Without Purpose")]
   public void GetSetPurpose_WithoutPurpose_ReturnsEmpty(ImportanceCombination value)
   {
      // Arrange
      ImportanceCombination expected = ImportanceCombination.Empty;

      // Act
      ImportanceCombination result = PurposeExtensions.GetSetPurpose(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }
   #endregion
   #region Get Purpose
   [DataRow(ImportanceCombination.Telemetry, ImportanceCombination.Telemetry, DisplayName = "Purpose -> Purpose")]
   [DataRow(ImportanceCombination.InheritPurpose, ImportanceCombination.InheritPurpose, DisplayName = "Inherit Purpose -> Inherit Purpose")]
   [DataRow(ImportanceCombination.NoPurpose, ImportanceCombination.NoPurpose, DisplayName = "No Purpose -> No Purpose")]
   [DataRow(ImportanceCombination.Negligible | ImportanceCombination.Telemetry, ImportanceCombination.Telemetry, DisplayName = "Combined -> Purpose")]
   [DataRow(ImportanceCombination.Inherit, ImportanceCombination.InheritPurpose, DisplayName = "Inherit -> Inherit Purpose")]
   [DataRow(ImportanceCombination.None, ImportanceCombination.NoPurpose, DisplayName = "None -> No Purpose")]
   [TestMethod("Get Purpose | With Purpose")]
   public void GetPurpose_WithPurpose_ReturnsExpectedPurpose(ImportanceCombination value, ImportanceCombination expected)
   {
      // Act
      ImportanceCombination result = PurposeExtensions.GetPurpose(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }

   [DataRow(ImportanceCombination.Empty, DisplayName = "Empty")]
   [DataRow(ImportanceCombination.Negligible, DisplayName = "Severity")]
   [DataRow(ImportanceCombination.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(ImportanceCombination.NoSeverity, DisplayName = "No Severity")]
   [TestMethod("Get Purpose | Without Purpose")]
   public void GetPurpose_WithoutPurpose_ReturnsNoPurpose(ImportanceCombination value)
   {
      // Arrange
      ImportanceCombination expected = ImportanceCombination.NoPurpose;

      // Act
      ImportanceCombination result = PurposeExtensions.GetPurpose(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }
   #endregion

   #region Has Purpose
   [DataRow(ImportanceCombination.Telemetry, DisplayName = "Purpose")]
   [DataRow(ImportanceCombination.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(ImportanceCombination.Negligible | ImportanceCombination.Telemetry, DisplayName = "Combined")]
   [DataRow(ImportanceCombination.Inherit, DisplayName = "Inherit")]
   [TestMethod("Has Purpose | With Purpose")]
   public void HasPurpose_WithPurpose_ReturnsTrue(ImportanceCombination value)
   {
      // Act
      bool result = PurposeExtensions.HasPurpose(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(ImportanceCombination.Empty, DisplayName = "Empty")]
   [DataRow(ImportanceCombination.NoPurpose, DisplayName = "No Purpose")]
   [DataRow(ImportanceCombination.Negligible, DisplayName = "Severity")]
   [DataRow(ImportanceCombination.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(ImportanceCombination.NoSeverity, DisplayName = "No Severity")]
   [DataRow(ImportanceCombination.None, DisplayName = "None")]
   [TestMethod("Has Purpose | Without Purpose")]
   public void HasPurpose_WithoutPurpose_ReturnsFalse(ImportanceCombination value)
   {
      // Act
      bool result = PurposeExtensions.HasPurpose(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion
   #endregion
}