namespace TNO.Common.Abstractions;

/// <summary>
/// A helper class for creating the purpose component of the <see cref="SeverityAndPurpose"/> enum.
/// </summary>
public static class Purpose
{
   #region Consts
   /// <summary>The bit mask used for the purpose component of the <see cref="SeverityAndPurpose"/> enum.</summary>
   public const byte BitMask = 0b1111_0000;
   #endregion

   #region Properties
   /// <inheritdoc cref="SeverityAndPurpose.Telemetry"/>
   public static SeverityAndPurpose Telemetry { get; } = SeverityAndPurpose.Telemetry;

   /// <inheritdoc cref="SeverityAndPurpose.Tracing"/>
   public static SeverityAndPurpose Tracing { get; } = SeverityAndPurpose.Tracing;

   /// <inheritdoc cref="SeverityAndPurpose.Diagnostics"/>
   public static SeverityAndPurpose Diagnostics { get; } = SeverityAndPurpose.Diagnostics;

   /// <inheritdoc cref="SeverityAndPurpose.Performance"/>
   public static SeverityAndPurpose Performance { get; } = SeverityAndPurpose.Performance;
   #endregion
}
