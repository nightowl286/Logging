using TNO.Logging.Common.Abstractions.Entries;
using TNO.Tests.Common;

namespace Common.Abstractions.Tests.entries;

[TestClass]
[TestCategory(Category.Importance)]
[TestCategory(Category.Extensions)]
public class ImportanceExtensionsTests
{
   #region Tests
   #region Is None
   [DataRow(Importance.Empty)]
   [DataRow(Importance.None)]
   [DataRow(Importance.NoPurpose)]
   [DataRow(Importance.NoSeverity)]
   [TestMethod]
   public void IsNone_WithValueEquivalentToNone_ReturnsTrue(Importance value)
   {
      // Act
      bool result = ImportanceExtensions.IsNone(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(Importance.Negligible)]
   [DataRow(Importance.InheritSeverity)]
   [DataRow(Importance.Telemetry)]
   [DataRow(Importance.InheritPurpose)]
   [TestMethod]
   public void IsNone_WithNotNoneValue_ReturnsFalse(Importance value)
   {
      // Act
      bool result = ImportanceExtensions.IsNone(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion

   #region Normalised
   [DataRow(Importance.NoPurpose)]
   [DataRow(Importance.Telemetry)]
   [DataRow(Importance.InheritPurpose)]
   [TestMethod]
   public void Normalised_WithNoSeverity_AddsSeverity(Importance value)
   {
      // Arrange Assert
      Assert.That.IsInconclusiveIf(value.IsSeveritySet(), $"The severity is already set on the given value ({value}).");
      Assert.That.IsInconclusiveIfNot(value.IsPurposeSet(), $"The purpose was never set on the given value ({value}).");

      // Act
      Importance result = ImportanceExtensions.Normalised(value);

      // Assert
      Assert.IsTrue(result.IsSeveritySet(), $"No severity has been added.");
      Assert.AreEqual(result.GetSetSeverity(), Importance.NoSeverity, $"The wrong severity has been set.");
      Assert.AreEqual(value.GetSetPurpose(), result.GetSetPurpose(), $"The purpose has changed when it shouldn't have.");
   }

   [DataRow(Importance.NoSeverity)]
   [DataRow(Importance.Negligible)]
   [DataRow(Importance.InheritSeverity)]
   [TestMethod]
   public void Normalised_WithNoPurpose_AddsPurpose(Importance value)
   {
      // Arrange Assert
      Assert.That.IsInconclusiveIf(value.IsPurposeSet(), $"The purpose is already set on the given value ({value}).");
      Assert.That.IsInconclusiveIfNot(value.IsSeveritySet(), $"The severity was never set on the given value ({value}).");

      // Act
      Importance result = ImportanceExtensions.Normalised(value);

      // Assert
      Assert.IsTrue(result.IsPurposeSet(), $"No purpose has been added.");
      Assert.AreEqual(result.GetSetPurpose(), Importance.NoPurpose, $"The wrong purpose has been set.");
      Assert.AreEqual(value.GetSetSeverity(), result.GetSetSeverity(), $"The severity has changed when it shouldn't have.");
   }

   [DataRow(Importance.None,
      DisplayName = "Normalised_AlreadyNormalisedValue_NoChanges (None)")]
   [DataRow(Importance.Negligible | Importance.Telemetry,
      DisplayName = "Normalised_AlreadyNormalisedValue_NoChanges (Combined)")]
   [DataRow(Importance.Inherit,
      DisplayName = "Normalised_AlreadyNormalisedValue_NoChanges (Inherit)")]
   [TestMethod]
   public void Normalised_AlreadyNormalisedValue_NoChanges(Importance value)
   {
      // Arrange Assert
      Assert.That.IsInconclusiveIfNot(value.IsSeveritySet(), $"No severity is set on the value ({value}).");
      Assert.That.IsInconclusiveIfNot(value.IsPurposeSet(), $"No purpose is set on the value ({value}).");

      // Act
      Importance result = ImportanceExtensions.Normalised(value);

      // Assert
      Assert.AreEqual(value.GetSetSeverity(), result.GetSetSeverity(), $"The severity has changed when it shouldn't have.");
      Assert.AreEqual(value.GetSetPurpose(), result.GetSetPurpose(), $"The purpose has changed when it shouldn't have.");
   }

   [TestMethod]
   public void Normalised_WithoutSeverityOrPurpose_AddsImportance()
   {
      // Arrange
      Importance value = Importance.Empty;
      Importance expected = Importance.None;

      // Act
      Importance result = ImportanceExtensions.Normalised(value);

      // Assert
      Assert.AreEqual(expected, result);
   }
   #endregion

   #region Normalise
   [DataRow(Importance.Empty)]
   [DataRow(Importance.None)]
   [DataRow(Importance.Negligible | Importance.Telemetry, DisplayName = "Combined")]
   [DataRow(Importance.Inherit)]
   [DataRow(Importance.NoSeverity)]
   [DataRow(Importance.Negligible)]
   [DataRow(Importance.InheritSeverity)]
   [DataRow(Importance.NoPurpose)]
   [DataRow(Importance.Telemetry)]
   [DataRow(Importance.InheritPurpose)]
   [TestMethod]
   public void Normalise_SetsCorrectValue(Importance value)
   {
      // Arrange
      Importance expected = ImportanceExtensions.Normalised(value);

      // Act
      value.Normalise();

      // Assert
      Assert.AreEqual(expected, value);
   }
   #endregion
   #endregion
}