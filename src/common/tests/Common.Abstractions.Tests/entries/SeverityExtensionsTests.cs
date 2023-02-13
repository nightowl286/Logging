using TNO.Logging.Common.Abstractions.Entries;

namespace Common.Abstractions.Tests.entries;

[TestClass]
[TestCategory(Category.Severity)]
[TestCategory(Category.Extensions)]
public class SeverityExtensionsTests
{
   #region Methods
   #region Is Severity Set
   [DataRow(Importance.NoSeverity, DisplayName = "No Severity")]
   [DataRow(Importance.Negligible, DisplayName = "Severity")]
   [DataRow(Importance.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(Importance.Negligible | Importance.Telemetry, DisplayName = "Combined")]
   [DataRow(Importance.Inherit, DisplayName = "Inherit")]
   [DataRow(Importance.None, DisplayName = "None")]
   [TestMethod("Is Severity Set | With Severity")]
   public void IsSeveritySet_WithSeverity_ReturnsTrue(Importance value)
   {
      // Act
      bool result = SeverityExtensions.IsSeveritySet(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(Importance.Empty, DisplayName = "Empty")]
   [DataRow(Importance.Telemetry, DisplayName = "Purpose Only")]
   [DataRow(Importance.NoPurpose, DisplayName = "No Purpose Only")]
   [DataRow(Importance.InheritPurpose, DisplayName = "Inherit Purpose Only")]
   [TestMethod("Is Severity Set | Without Severity")]
   public void IsSeveritySet_WithoutSeverity_ReturnsFalse(Importance value)
   {
      // Act
      bool result = SeverityExtensions.IsSeveritySet(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion

   #region Get Set Severity
   [DataRow(Importance.Negligible, Importance.Negligible, DisplayName = "Severity -> Severity")]
   [DataRow(Importance.InheritSeverity, Importance.InheritSeverity, DisplayName = "Inherit Severity -> Inherit Severity")]
   [DataRow(Importance.NoSeverity, Importance.NoSeverity, DisplayName = "No Severity -> No Severity")]
   [DataRow(Importance.Negligible | Importance.Telemetry, Importance.Negligible, DisplayName = "Combined -> Severity")]
   [DataRow(Importance.Inherit, Importance.InheritSeverity, DisplayName = "Inherit -> Inherit Severity")]
   [DataRow(Importance.None, Importance.NoSeverity, DisplayName = "None -> No Severity")]
   [TestMethod("Get Set Severity | With Severity")]
   public void GetSetSeverity_WithSeverity_ReturnsExpectedSeverity(Importance value, Importance expected)
   {
      // Act
      Importance result = SeverityExtensions.GetSetSeverity(value);

      // Assert
      Assert.AreEqual(expected, result);
   }

   [DataRow(Importance.Empty, DisplayName = "Empty")]
   [DataRow(Importance.Telemetry, DisplayName = "Purpose")]
   [DataRow(Importance.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(Importance.NoPurpose, DisplayName = "No Purpose")]
   [TestMethod("Get Set Severity | Without Severity")]
   public void GetSetSeverity_WithoutSeverity_ReturnsEmpty(Importance value)
   {
      // Arrange
      Importance expected = Importance.Empty;

      // Act
      Importance result = SeverityExtensions.GetSetSeverity(value);

      // Assert
      Assert.AreEqual(expected, result);
   }
   #endregion
   #region Get Severity
   [DataRow(Importance.Negligible, Importance.Negligible, DisplayName = "Severity -> Severity")]
   [DataRow(Importance.InheritSeverity, Importance.InheritSeverity, DisplayName = "Inherit Severity -> Inherit Severity")]
   [DataRow(Importance.NoSeverity, Importance.NoSeverity, DisplayName = "No Severity -> No Severity")]
   [DataRow(Importance.Negligible | Importance.Telemetry, Importance.Negligible, DisplayName = "Combined -> Severity")]
   [DataRow(Importance.Inherit, Importance.InheritSeverity, DisplayName = "Inherit -> Inherit Severity")]
   [DataRow(Importance.None, Importance.NoSeverity, DisplayName = "None -> No Severity")]
   [TestMethod("Get Severity | With Severity")]
   public void GetSeverity_WithSeverity_ReturnsExpectedSeverity(Importance value, Importance expected)
   {
      // Act
      Importance result = SeverityExtensions.GetSeverity(value);

      // Assert
      Assert.AreEqual(expected, result);
   }

   [DataRow(Importance.Empty, DisplayName = "Empty")]
   [DataRow(Importance.Telemetry, DisplayName = "Purpose")]
   [DataRow(Importance.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(Importance.NoPurpose, DisplayName = "No Purpose")]
   [TestMethod("Get Severity | Without Severity")]
   public void GetSeverity_WithoutSeverity_ReturnsNoSeverity(Importance value)
   {
      // Arrange
      Importance expected = Importance.NoSeverity;

      // Act
      Importance result = SeverityExtensions.GetSeverity(value);

      // Assert
      Assert.AreEqual(expected, result);
   }
   #endregion

   #region Has Severity
   [DataRow(Importance.Negligible, DisplayName = "Severity")]
   [DataRow(Importance.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(Importance.Negligible | Importance.Telemetry, DisplayName = "Combined")]
   [DataRow(Importance.Inherit, DisplayName = "Inherit")]
   [TestMethod("Has Severity | With Severity")]
   public void HasSeverity_WithSeverity_ReturnsTrue(Importance value)
   {
      // Act
      bool result = SeverityExtensions.HasSeverity(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(Importance.Empty, DisplayName = "Empty")]
   [DataRow(Importance.NoSeverity, DisplayName = "No Severity")]
   [DataRow(Importance.Telemetry, DisplayName = "Purpose")]
   [DataRow(Importance.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(Importance.NoPurpose, DisplayName = "No Purpose")]
   [DataRow(Importance.None, DisplayName = "None")]
   [TestMethod("Has Severity | Without Severity")]
   public void HasSeverity_WithoutSeverity_ReturnsFalse(Importance value)
   {
      // Act
      bool result = SeverityExtensions.HasSeverity(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion
   #endregion
}
