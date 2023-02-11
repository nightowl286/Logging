using TNO.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Tests.Common;

namespace Common.Abstractions.Tests.entries;

[TestClass]
[TestCategory(Category.Severity)]
[TestCategory(Category.Purpose)]
public class SeverityAndPurposeExtensionsTests
{
   #region Tests
   #region Is None
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

   #region Normalised
   [DataRow(SeverityAndPurpose.NoPurpose)]
   [DataRow(SeverityAndPurpose.Telemetry)]
   [DataRow(SeverityAndPurpose.InheritPurpose)]
   [TestMethod]
   public void Normalised_WithNoSeverity_AddsSeverity(SeverityAndPurpose value)
   {
      // Arrange Assert
      Assert.That.IsInconclusiveIf(value.IsSeveritySet(), $"The severity is already set on the given value ({value}).");
      Assert.That.IsInconclusiveIfNot(value.IsPurposeSet(), $"The purpose was never set on the given value ({value}).");

      // Act
      SeverityAndPurpose result = SeverityAndPurposeExtensions.Normalised(value);

      // Assert
      Assert.IsTrue(result.IsSeveritySet(), $"No severity has been added.");
      Assert.AreEqual(result.GetSetSeverity(), SeverityAndPurpose.NoSeverity, $"The wrong severity has been set.");
      Assert.AreEqual(value.GetSetPurpose(), result.GetSetPurpose(), $"The purpose has changed when it shouldn't have.");
   }

   [DataRow(SeverityAndPurpose.NoSeverity)]
   [DataRow(SeverityAndPurpose.Negligible)]
   [DataRow(SeverityAndPurpose.InheritSeverity)]
   [TestMethod]
   public void Normalised_WithNoPurpose_AddsPurpose(SeverityAndPurpose value)
   {
      // Arrange Assert
      Assert.That.IsInconclusiveIf(value.IsPurposeSet(), $"The purpose is already set on the given value ({value}).");
      Assert.That.IsInconclusiveIfNot(value.IsSeveritySet(), $"The severity was never set on the given value ({value}).");

      // Act
      SeverityAndPurpose result = SeverityAndPurposeExtensions.Normalised(value);

      // Assert
      Assert.IsTrue(result.IsPurposeSet(), $"No purpose has been added.");
      Assert.AreEqual(result.GetSetPurpose(), SeverityAndPurpose.NoPurpose, $"The wrong purpose has been set.");
      Assert.AreEqual(value.GetSetSeverity(), result.GetSetSeverity(), $"The severity has changed when it shouldn't have.");
   }

   [DataRow(SeverityAndPurpose.None,
      DisplayName = "Normalised_AlreadyNormalisedValue_NoChanges (None)")]
   [DataRow(SeverityAndPurpose.Negligible | SeverityAndPurpose.Telemetry,
      DisplayName = "Normalised_AlreadyNormalisedValue_NoChanges (Combined)")]
   [DataRow(SeverityAndPurpose.Inherit, 
      DisplayName = "Normalised_AlreadyNormalisedValue_NoChanges (Inherit)")]
   [TestMethod]
   public void Normalised_AlreadyNormalisedValue_NoChanges(SeverityAndPurpose value)
   {
      // Arrange Assert
      Assert.That.IsInconclusiveIfNot(value.IsSeveritySet(), $"No severity is set on the value ({value}).");
      Assert.That.IsInconclusiveIfNot(value.IsPurposeSet(), $"No purpose is set on the value ({value}).");

      // Act
      SeverityAndPurpose result = SeverityAndPurposeExtensions.Normalised(value);

      // Assert
      Assert.AreEqual(value.GetSetSeverity(), result.GetSetSeverity(), $"The severity has changed when it shouldn't have.");
      Assert.AreEqual(value.GetSetPurpose(), result.GetSetPurpose(), $"The purpose has changed when it shouldn't have.");
   }

   [TestMethod]
   public void Normalised_WithoutSeverityOrPurpose_AddsSeverityAndPurpose()
   {
      // Arrange
      SeverityAndPurpose value = SeverityAndPurpose.Empty;
      SeverityAndPurpose expected = SeverityAndPurpose.None;

      // Act
      SeverityAndPurpose result = SeverityAndPurposeExtensions.Normalised(value);

      // Assert
      Assert.AreEqual(expected, result);
   }
   #endregion

   #region Normalise
   [DataRow(SeverityAndPurpose.Empty)]
   [DataRow(SeverityAndPurpose.None)]
   [DataRow(SeverityAndPurpose.Negligible | SeverityAndPurpose.Telemetry, DisplayName = "Combined")]
   [DataRow(SeverityAndPurpose.Inherit)]
   [DataRow(SeverityAndPurpose.NoSeverity)]
   [DataRow(SeverityAndPurpose.Negligible)]
   [DataRow(SeverityAndPurpose.InheritSeverity)]
   [DataRow(SeverityAndPurpose.NoPurpose)]
   [DataRow(SeverityAndPurpose.Telemetry)]
   [DataRow(SeverityAndPurpose.InheritPurpose)]
   [TestMethod]
   public void Normalise_SetsCorrectValue(SeverityAndPurpose value)
   {
      // Arrange
      SeverityAndPurpose expected = SeverityAndPurposeExtensions.Normalised(value);

      // Act
      value.Normalise();

      // Assert
      Assert.AreEqual(expected, value);
   }
   #endregion
   #endregion
}
