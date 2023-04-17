using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Tests.Common;

namespace Common.Abstractions.Tests.entries;

[TestClass]
[TestCategory(Category.Importance)]
[TestCategory(Category.Extensions)]
public class ImportanceExtensionsTests
{
   #region Tests
   #region Is None
   [DataRow(ImportanceCombination.Empty)]
   [DataRow(ImportanceCombination.None)]
   [DataRow(ImportanceCombination.NoPurpose)]
   [DataRow(ImportanceCombination.NoSeverity)]
   [TestMethod]
   public void IsNone_WithValueEquivalentToNone_ReturnsTrue(ImportanceCombination value)
   {
      // Act
      bool result = ImportanceExtensions.IsNone(value);

      // Assert
      Assert.IsTrue(result);
   }

   [DataRow(ImportanceCombination.Negligible)]
   [DataRow(ImportanceCombination.InheritSeverity)]
   [DataRow(ImportanceCombination.Telemetry)]
   [DataRow(ImportanceCombination.InheritPurpose)]
   [TestMethod]
   public void IsNone_WithNotNoneValue_ReturnsFalse(ImportanceCombination value)
   {
      // Act
      bool result = ImportanceExtensions.IsNone(value);

      // Assert
      Assert.IsFalse(result);
   }
   #endregion

   #region Normalised
   [DataRow(ImportanceCombination.NoPurpose)]
   [DataRow(ImportanceCombination.Telemetry)]
   [DataRow(ImportanceCombination.InheritPurpose)]
   [TestMethod]
   public void Normalised_WithNoSeverity_AddsSeverity(ImportanceCombination value)
   {
      // Arrange Assert
      Assert.That.IsInconclusiveIf(value.IsSeveritySet(), $"The severity is already set on the given value ({value}).");
      Assert.That.IsInconclusiveIfNot(value.IsPurposeSet(), $"The purpose was never set on the given value ({value}).");

      // Act
      ImportanceCombination result = ImportanceExtensions.Normalised(value);

      // Assert
      Assert.IsTrue(result.IsSeveritySet(), $"No severity has been added.");
      Assert.That.AreEqual(result.GetSetSeverity(), ImportanceCombination.NoSeverity, $"The wrong severity has been set.");
      Assert.That.AreEqual(value.GetSetPurpose(), result.GetSetPurpose(), $"The purpose has changed when it shouldn't have.");
   }

   [DataRow(ImportanceCombination.NoSeverity)]
   [DataRow(ImportanceCombination.Negligible)]
   [DataRow(ImportanceCombination.InheritSeverity)]
   [TestMethod]
   public void Normalised_WithNoPurpose_AddsPurpose(ImportanceCombination value)
   {
      // Arrange Assert
      Assert.That.IsInconclusiveIf(value.IsPurposeSet(), $"The purpose is already set on the given value ({value}).");
      Assert.That.IsInconclusiveIfNot(value.IsSeveritySet(), $"The severity was never set on the given value ({value}).");

      // Act
      ImportanceCombination result = ImportanceExtensions.Normalised(value);

      // Assert
      Assert.IsTrue(result.IsPurposeSet(), $"No purpose has been added.");
      Assert.That.AreEqual(result.GetSetPurpose(), ImportanceCombination.NoPurpose, $"The wrong purpose has been set.");
      Assert.That.AreEqual(value.GetSetSeverity(), result.GetSetSeverity(), $"The severity has changed when it shouldn't have.");
   }

   [DataRow(ImportanceCombination.None,
      DisplayName = "Normalised_AlreadyNormalisedValue_NoChanges (None)")]
   [DataRow(ImportanceCombination.Negligible | ImportanceCombination.Telemetry,
      DisplayName = "Normalised_AlreadyNormalisedValue_NoChanges (Combined)")]
   [DataRow(ImportanceCombination.Inherit,
      DisplayName = "Normalised_AlreadyNormalisedValue_NoChanges (Inherit)")]
   [TestMethod]
   public void Normalised_AlreadyNormalisedValue_NoChanges(ImportanceCombination value)
   {
      // Arrange Assert
      Assert.That.IsInconclusiveIfNot(value.IsSeveritySet(), $"No severity is set on the value ({value}).");
      Assert.That.IsInconclusiveIfNot(value.IsPurposeSet(), $"No purpose is set on the value ({value}).");

      // Act
      ImportanceCombination result = ImportanceExtensions.Normalised(value);

      // Assert
      Assert.That.AreEqual(value.GetSetSeverity(), result.GetSetSeverity(), $"The severity has changed when it shouldn't have.");
      Assert.That.AreEqual(value.GetSetPurpose(), result.GetSetPurpose(), $"The purpose has changed when it shouldn't have.");
   }

   [TestMethod]
   public void Normalised_WithoutSeverityOrPurpose_AddsImportance()
   {
      // Arrange
      ImportanceCombination value = ImportanceCombination.Empty;
      ImportanceCombination expected = ImportanceCombination.None;

      // Act
      ImportanceCombination result = ImportanceExtensions.Normalised(value);

      // Assert
      Assert.That.AreEqual(expected, result);
   }
   #endregion

   #region Normalise
   [DataRow(ImportanceCombination.Empty)]
   [DataRow(ImportanceCombination.None)]
   [DataRow(ImportanceCombination.Negligible | ImportanceCombination.Telemetry, DisplayName = "Combined")]
   [DataRow(ImportanceCombination.Inherit)]
   [DataRow(ImportanceCombination.NoSeverity)]
   [DataRow(ImportanceCombination.Negligible)]
   [DataRow(ImportanceCombination.InheritSeverity)]
   [DataRow(ImportanceCombination.NoPurpose)]
   [DataRow(ImportanceCombination.Telemetry)]
   [DataRow(ImportanceCombination.InheritPurpose)]
   [TestMethod]
   public void Normalise_SetsCorrectValue(ImportanceCombination value)
   {
      // Arrange
      ImportanceCombination expected = ImportanceExtensions.Normalised(value);

      // Act
      value.Normalise();

      // Assert
      Assert.That.AreEqual(expected, value);
   }
   #endregion
   #endregion
}