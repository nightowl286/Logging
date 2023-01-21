namespace TNO.Common.Abstractions;

/// <summary>
/// A helper class for creating the severity component of the <see cref="SeverityAndPurpose"/> enum.
/// </summary>
public static class Severity
{
   #region Consts
   /// <summary>The bit mask used for the severity component of the <see cref="SeverityAndPurpose"/> enum.</summary>
   public const byte BitMask = 0b0000_1111;
   #endregion

   #region Properties
   /// <inheritdoc cref="SeverityAndPurpose.Negligible"/>
   public static SeverityAndPurpose Negligible { get; } = SeverityAndPurpose.Negligible;

   /// <inheritdoc cref="SeverityAndPurpose.Substantial"/>
   public static SeverityAndPurpose Substantial { get; } = SeverityAndPurpose.Substantial;

   /// <inheritdoc cref="SeverityAndPurpose.Critical"/>
   public static SeverityAndPurpose Critical { get; } = SeverityAndPurpose.Critical;

   /// <inheritdoc cref="SeverityAndPurpose.Fatal"/>
   public static SeverityAndPurpose Fatal { get; } = SeverityAndPurpose.Fatal;
   #endregion
}
