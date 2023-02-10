using System.ComponentModel;
using System.Reflection;
using TNO.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Tests.Common;
using Enum = TNO.Logging.Common.Abstractions.Entries.SeverityAndPurpose;

namespace Common.Abstractions.Tests.entries;

[TestClass]
[TestCategory(Category.Purpose)]
public sealed class PurposeTests
{
   #region Tests
   #region Is Purpose Set
   [DataRow(Enum.NoPurpose, DisplayName = "No Purpose")]
   [DataRow(Enum.Telemetry, DisplayName = "Purpose")]
   [DataRow(Enum.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(Enum.Negligible | Enum.Telemetry, DisplayName = "Combined")]
   [DataRow(Enum.Inherit, DisplayName = "Inherit")]
   [DataRow(Enum.None, DisplayName = "None")]
   [TestMethod("Is Purpose Set | With Purpose")]
   public void IsPurposeSet_WithPurpose_ReturnsTrue(Enum value)
   {
      // Act
      bool result = Purpose.IsPurposeSet(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(Enum.Empty, DisplayName = "Empty")]
   [DataRow(Enum.Negligible, DisplayName = "Severity Only")]
   [DataRow(Enum.NoSeverity, DisplayName = "No Severity Only")]
   [DataRow(Enum.InheritSeverity, DisplayName = "Inherit Severity Only")]
   [TestMethod("Is Purpose Set | Without Purpose")]
   public void IsPurposeSet_WithoutPurpose_ReturnsFalse(Enum value)
   {
      // Act
      bool result = Purpose.IsPurposeSet(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion

   #region Get Set Purpose
   [DataRow(Enum.Telemetry, Enum.Telemetry, DisplayName = "Purpose -> Purpose")]
   [DataRow(Enum.InheritPurpose, Enum.InheritPurpose, DisplayName = "Inherit Purpose -> Inherit Purpose")]
   [DataRow(Enum.NoPurpose, Enum.NoPurpose, DisplayName = "No Purpose -> No Purpose")]
   [DataRow(Enum.Negligible | Enum.Telemetry, Enum.Telemetry, DisplayName = "Combined -> Purpose")]
   [DataRow(Enum.Inherit, Enum.InheritPurpose, DisplayName = "Inherit -> Inherit Purpose")]
   [DataRow(Enum.None, Enum.NoPurpose, DisplayName = "None -> No Purpose")]
   [TestMethod("Get Set Purpose | With Purpose")]
   public void GetSetPurpose_WithPurpose_ReturnsExpectedPurpose(Enum value, Enum expected)
   {
      // Act
      Enum result = Purpose.GetSetPurpose(value);

      // Assert
      Assert.AreEqual(expected, result);
   }

   [DataRow(Enum.Empty, DisplayName = "Empty")]
   [DataRow(Enum.Negligible, DisplayName = "Severity")]
   [DataRow(Enum.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(Enum.NoSeverity, DisplayName = "No Severity")]
   [TestMethod("Get Set Purpose | Without Purpose")]
   public void GetSetPurpose_WithoutPurpose_ReturnsEmpty(Enum value)
   {
      // Arrange
      Enum expected = Enum.Empty;

      // Act
      Enum result = Purpose.GetSetPurpose(value);

      // Assert
      Assert.AreEqual(expected, result);
   }
   #endregion
   #region Get Purpose
   [DataRow(Enum.Telemetry, Enum.Telemetry, DisplayName = "Purpose -> Purpose")]
   [DataRow(Enum.InheritPurpose, Enum.InheritPurpose, DisplayName = "Inherit Purpose -> Inherit Purpose")]
   [DataRow(Enum.NoPurpose, Enum.NoPurpose, DisplayName = "No Purpose -> No Purpose")]
   [DataRow(Enum.Negligible | Enum.Telemetry, Enum.Telemetry, DisplayName = "Combined -> Purpose")]
   [DataRow(Enum.Inherit, Enum.InheritPurpose, DisplayName = "Inherit -> Inherit Purpose")]
   [DataRow(Enum.None, Enum.NoPurpose, DisplayName = "None -> No Purpose")]
   [TestMethod("Get Purpose | With Purpose")]
   public void GetPurpose_WithPurpose_ReturnsExpectedPurpose(Enum value, Enum expected)
   {
      // Act
      Enum result = Purpose.GetPurpose(value);

      // Assert
      Assert.AreEqual(expected, result);
   }

   [DataRow(Enum.Empty, DisplayName = "Empty")]
   [DataRow(Enum.Negligible, DisplayName = "Severity")]
   [DataRow(Enum.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(Enum.NoSeverity, DisplayName = "No Severity")]
   [TestMethod("Get Purpose | Without Purpose")]
   public void GetPurpose_WithoutPurpose_ReturnsNoPurpose(Enum value)
   {
      // Arrange
      Enum expected = Enum.NoPurpose;

      // Act
      Enum result = Purpose.GetPurpose(value);

      // Assert
      Assert.AreEqual(expected, result);
   }
   #endregion

   #region Has Purpose
   [DataRow(Enum.Telemetry, DisplayName = "Purpose")]
   [DataRow(Enum.InheritPurpose, DisplayName = "Inherit Purpose")]
   [DataRow(Enum.Negligible | Enum.Telemetry, DisplayName = "Combined")]
   [DataRow(Enum.Inherit, DisplayName = "Inherit")]
   [TestMethod("Has Purpose | With Purpose")]
   public void HasPurpose_WithPurpose_ReturnsTrue(Enum value)
   {
      // Act
      bool result = Purpose.HasPurpose(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(Enum.Empty, DisplayName = "Empty")]
   [DataRow(Enum.NoPurpose, DisplayName = "No Purpose")]
   [DataRow(Enum.Negligible, DisplayName = "Severity")]
   [DataRow(Enum.InheritSeverity, DisplayName = "Inherit Severity")]
   [DataRow(Enum.NoSeverity, DisplayName = "No Severity")]
   [DataRow(Enum.None, DisplayName = "None")]
   [TestMethod("Has Purpose | Without Purpose")]
   public void HasPurpose_WithoutPurpose_ReturnsFalse(Enum value)
   {
      // Act
      bool result = Purpose.HasPurpose(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion

   [TestMethod]
   public void GetAll_CountMatchesPropertyCount()
   {
      // Arrange
      int expected = typeof(Purpose)
         .GetProperties(BindingFlags.Public | BindingFlags.Static)
         .Length;

      // Act
      Enum[] values = Purpose.GetAll().ToArray();

      // Assert
      Assert.That.IsInconclusiveIf(values.Length != expected,
         "It is likely that this result has cascaded, please check the other tests (and code tests) first.");

   }
   #endregion
}
