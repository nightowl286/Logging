using TNO.Logging.Common.Abstractions.Entries;

namespace Common.Abstractions.Tests.entries;

[TestClass]
[TestCategory(Category.Purpose)]
[TestCategory(Category.Extensions)]
public class PurposeExtensionsTests
{
   #region Tests
   #region Is Purpose Set
   [DataRow(Importance.NoPurpose, DisplayName = "No Purpose")]
   [DataRow(Importance.Telemetry, DisplayName = "Purpose")]
   [DataRow(Importance.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(Importance.Negligible | Importance.Telemetry, DisplayName = "Combined")]
   [DataRow(Importance.Inherit, DisplayName = "Inherit")]
   [DataRow(Importance.None, DisplayName = "None")]
   [TestMethod("Is Purpose Set | With Purpose")]
   public void IsPurposeSet_WithPurpose_ReturnsTrue(Importance value)
   {
      // Act
      bool result = PurposeExtensions.IsPurposeSet(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(Importance.Empty, DisplayName = "Empty")]
   [DataRow(Importance.Negligible, DisplayName = "Severity Only")]
   [DataRow(Importance.NoSeverity, DisplayName = "No Severity Only")]
   [DataRow(Importance.InheritSeverity, DisplayName = "Inherit Severity Only")]
   [TestMethod("Is Purpose Set | Without Purpose")]
   public void IsPurposeSet_WithoutPurpose_ReturnsFalse(Importance value)
   {
      // Act
      bool result = PurposeExtensions.IsPurposeSet(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion

   #region Get Set Purpose
   [DataRow(Importance.Telemetry, Importance.Telemetry, DisplayName = "Purpose -> Purpose")]
   [DataRow(Importance.InheritPurpose, Importance.InheritPurpose, DisplayName = "Inherit Purpose -> Inherit Purpose")]
   [DataRow(Importance.NoPurpose, Importance.NoPurpose, DisplayName = "No Purpose -> No Purpose")]
   [DataRow(Importance.Negligible | Importance.Telemetry, Importance.Telemetry, DisplayName = "Combined -> Purpose")]
   [DataRow(Importance.Inherit, Importance.InheritPurpose, DisplayName = "Inherit -> Inherit Purpose")]
   [DataRow(Importance.None, Importance.NoPurpose, DisplayName = "None -> No Purpose")]
   [TestMethod("Get Set Purpose | With Purpose")]
   public void GetSetPurpose_WithPurpose_ReturnsExpectedPurpose(Importance value, Importance expected)
   {
      // Act
      Importance result = PurposeExtensions.GetSetPurpose(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }

   [DataRow(Importance.Empty, DisplayName = "Empty")]
   [DataRow(Importance.Negligible, DisplayName = "Severity")]
   [DataRow(Importance.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(Importance.NoSeverity, DisplayName = "No Severity")]
   [TestMethod("Get Set Purpose | Without Purpose")]
   public void GetSetPurpose_WithoutPurpose_ReturnsEmpty(Importance value)
   {
      // Arrange
      Importance expected = Importance.Empty;

      // Act
      Importance result = PurposeExtensions.GetSetPurpose(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }
   #endregion
   #region Get Purpose
   [DataRow(Importance.Telemetry, Importance.Telemetry, DisplayName = "Purpose -> Purpose")]
   [DataRow(Importance.InheritPurpose, Importance.InheritPurpose, DisplayName = "Inherit Purpose -> Inherit Purpose")]
   [DataRow(Importance.NoPurpose, Importance.NoPurpose, DisplayName = "No Purpose -> No Purpose")]
   [DataRow(Importance.Negligible | Importance.Telemetry, Importance.Telemetry, DisplayName = "Combined -> Purpose")]
   [DataRow(Importance.Inherit, Importance.InheritPurpose, DisplayName = "Inherit -> Inherit Purpose")]
   [DataRow(Importance.None, Importance.NoPurpose, DisplayName = "None -> No Purpose")]
   [TestMethod("Get Purpose | With Purpose")]
   public void GetPurpose_WithPurpose_ReturnsExpectedPurpose(Importance value, Importance expected)
   {
      // Act
      Importance result = PurposeExtensions.GetPurpose(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }

   [DataRow(Importance.Empty, DisplayName = "Empty")]
   [DataRow(Importance.Negligible, DisplayName = "Severity")]
   [DataRow(Importance.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(Importance.NoSeverity, DisplayName = "No Severity")]
   [TestMethod("Get Purpose | Without Purpose")]
   public void GetPurpose_WithoutPurpose_ReturnsNoPurpose(Importance value)
   {
      // Arrange
      Importance expected = Importance.NoPurpose;

      // Act
      Importance result = PurposeExtensions.GetPurpose(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }
   #endregion

   #region Has Purpose
   [DataRow(Importance.Telemetry, DisplayName = "Purpose")]
   [DataRow(Importance.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(Importance.Negligible | Importance.Telemetry, DisplayName = "Combined")]
   [DataRow(Importance.Inherit, DisplayName = "Inherit")]
   [TestMethod("Has Purpose | With Purpose")]
   public void HasPurpose_WithPurpose_ReturnsTrue(Importance value)
   {
      // Act
      bool result = PurposeExtensions.HasPurpose(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(Importance.Empty, DisplayName = "Empty")]
   [DataRow(Importance.NoPurpose, DisplayName = "No Purpose")]
   [DataRow(Importance.Negligible, DisplayName = "Severity")]
   [DataRow(Importance.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(Importance.NoSeverity, DisplayName = "No Severity")]
   [DataRow(Importance.None, DisplayName = "None")]
   [TestMethod("Has Purpose | Without Purpose")]
   public void HasPurpose_WithoutPurpose_ReturnsFalse(Importance value)
   {
      // Act
      bool result = PurposeExtensions.HasPurpose(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion
   #endregion
}