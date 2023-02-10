using System.Reflection;
using TNO.Common.Abstractions;
using TNO.Tests.Common;
using Enum = TNO.Logging.Common.Abstractions.Entries.SeverityAndPurpose;

namespace Common.Abstractions.Tests.entries;

[TestClass]
[TestCategory(Category.Severity)]
public sealed class SeverityTests
{
   #region Tests
   #region Is Severity Set
   [DataRow(Enum.NoSeverity, DisplayName = "No Severity")]
   [DataRow(Enum.Negligible, DisplayName = "Severity")]
   [DataRow(Enum.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(Enum.Negligible | Enum.Telemetry, DisplayName = "Combined")]
   [DataRow(Enum.Inherit, DisplayName = "Inherit")]
   [DataRow(Enum.None, DisplayName = "None")]
   [TestMethod("Is Severity Set | With Severity")]
   public void IsSeveritySet_WithSeverity_ReturnsTrue(Enum value)
   {
      // Act
      bool result = Severity.IsSeveritySet(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(Enum.Empty, DisplayName = "Empty")]
   [DataRow(Enum.Telemetry, DisplayName = "Purpose Only")]
   [DataRow(Enum.NoPurpose, DisplayName = "No Purpose Only")]
   [DataRow(Enum.InheritPurpose, DisplayName = "Inherit Purpose Only")]
   [TestMethod("Is Severity Set | Without Severity")]
   public void IsSeveritySet_WithoutSeverity_ReturnsFalse(Enum value)
   {
      // Act
      bool result = Severity.IsSeveritySet(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion

   #region Get Set Severity
   [DataRow(Enum.Negligible, Enum.Negligible, DisplayName = "Severity -> Severity")]
   [DataRow(Enum.InheritSeverity, Enum.InheritSeverity, DisplayName = "Inherit Severity -> Inherit Severity")]
   [DataRow(Enum.NoSeverity, Enum.NoSeverity, DisplayName = "No Severity -> No Severity")]
   [DataRow(Enum.Negligible | Enum.Telemetry, Enum.Negligible, DisplayName = "Combined -> Severity")]
   [DataRow(Enum.Inherit, Enum.InheritSeverity, DisplayName = "Inherit -> Inherit Severity")]
   [DataRow(Enum.None, Enum.NoSeverity, DisplayName = "None -> No Severity")]
   [TestMethod("Get Set Severity | With Severity")]
   public void GetSetSeverity_WithSeverity_ReturnsExpectedSeverity(Enum value, Enum expected)
   {
      // Act
      Enum result = Severity.GetSetSeverity(value);

      // Assert
      Assert.AreEqual(expected, result);
   }

   [DataRow(Enum.Empty, DisplayName = "Empty")]
   [DataRow(Enum.Telemetry, DisplayName = "Purpose")]
   [DataRow(Enum.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(Enum.NoPurpose, DisplayName = "No Purpose")]
   [TestMethod("Get Set Severity | Without Severity")]
   public void GetSetSeverity_WithoutSeverity_ReturnsEmpty(Enum value)
   {
      // Arrange
      Enum expected = Enum.Empty;

      // Act
      Enum result = Severity.GetSetSeverity(value);

      // Assert
      Assert.AreEqual(expected, result);
   }
   #endregion
   #region Get Severity
   [DataRow(Enum.Negligible, Enum.Negligible, DisplayName = "Severity -> Severity")]
   [DataRow(Enum.InheritSeverity, Enum.InheritSeverity, DisplayName = "Inherit Severity -> Inherit Severity")]
   [DataRow(Enum.NoSeverity, Enum.NoSeverity, DisplayName = "No Severity -> No Severity")]
   [DataRow(Enum.Negligible | Enum.Telemetry, Enum.Negligible, DisplayName = "Combined -> Severity")]
   [DataRow(Enum.Inherit, Enum.InheritSeverity, DisplayName = "Inherit -> Inherit Severity")]
   [DataRow(Enum.None, Enum.NoSeverity, DisplayName = "None -> No Severity")]
   [TestMethod("Get Severity | With Severity")]
   public void GetSeverity_WithSeverity_ReturnsExpectedSeverity(Enum value, Enum expected)
   {
      // Act
      Enum result = Severity.GetSeverity(value);

      // Assert
      Assert.AreEqual(expected, result);
   }

   [DataRow(Enum.Empty, DisplayName = "Empty")]
   [DataRow(Enum.Telemetry, DisplayName = "Purpose")]
   [DataRow(Enum.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(Enum.NoPurpose, DisplayName = "No Purpose")]
   [TestMethod("Get Severity | Without Severity")]
   public void GetSeverity_WithoutSeverity_ReturnsNoSeverity(Enum value)
   {
      // Arrange
      Enum expected = Enum.NoSeverity;

      // Act
      Enum result = Severity.GetSeverity(value);

      // Assert
      Assert.AreEqual(expected, result);
   }
   #endregion

   #region Has Severity
   [DataRow(Enum.Negligible, DisplayName = "Severity")]
   [DataRow(Enum.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(Enum.Negligible | Enum.Telemetry, DisplayName = "Combined")]
   [DataRow(Enum.Inherit, DisplayName = "Inherit")]
   [TestMethod("Has Severity | With Severity")]
   public void HasSeverity_WithSeverity_ReturnsTrue(Enum value)
   {
      // Act
      bool result = Severity.HasSeverity(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(Enum.Empty, DisplayName = "Empty")]
   [DataRow(Enum.NoSeverity, DisplayName = "No Severity")]
   [DataRow(Enum.Telemetry, DisplayName = "Purpose")]
   [DataRow(Enum.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(Enum.NoPurpose, DisplayName = "No Purpose")]
   [DataRow(Enum.None, DisplayName = "None")]
   [TestMethod("Has Severity | Without Severity")]
   public void HasSeverity_WithoutSeverity_ReturnsFalse(Enum value)
   {
      // Act
      bool result = Severity.HasSeverity(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion

   [TestMethod]
   public void GetAll_CountMatchesPropertyCount()
   {
      // Arrange
      int expected = typeof(Severity)
         .GetProperties(BindingFlags.Public | BindingFlags.Static)
         .Length;

      // Act
      Enum[] values = Severity.GetAll().ToArray();

      // Assert
      Assert.That.IsInconclusiveIf(values.Length != expected,
         "It is likely that this result has cascaded, please check the other tests (and code tests) first.");

   }
   #endregion
}
