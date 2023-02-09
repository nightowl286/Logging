using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using TNO.Logging.Common.Abstractions.Entries;

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

   #region Methods
   /// <summary>Checks whether the given <paramref name="value"/> has a flag set for the purpose.</summary>
   /// <param name="value">The purpose value to check.</param>
   /// <returns><see langword="true"/> if a purpose flag is set, <see langword="false"/> otherwise.</returns>
   public static bool IsPurposeSet(this SeverityAndPurpose value)
      => GetSetPurpose(value) != SeverityAndPurpose.None;

   /// <summary>Gets the purpose flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the purpose flag of.</param>
   /// <returns>
   /// The purpose flag that was set on the given <paramref name="value"/>,
   /// or <see cref="SeverityAndPurpose.Empty"/> if no purpose flag wasn't set.
   /// </returns>
   public static SeverityAndPurpose GetSetPurpose(this SeverityAndPurpose value)
   {
      byte masked = (byte)(((byte)value) & BitMask);

      return (SeverityAndPurpose)masked;
   }

   /// <summary>Gets the purpose flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the purpose flag of.</param>
   /// <returns>
   /// The purpose flag that was set on the given <paramref name="value"/>,
   /// or <see cref="SeverityAndPurpose.NoPurpose"/> if no purpose flag wasn't set.
   /// </returns>
   public static SeverityAndPurpose GetPurpose(this SeverityAndPurpose value)
   {
      value = GetSetPurpose(value);

      return value == SeverityAndPurpose.Empty ? SeverityAndPurpose.NoPurpose : value;
   }

   /// <summary>
   /// Checks whether the given <paramref name="value"/> has a flag set 
   /// for purpose, and it is not <see cref="SeverityAndPurpose.NoPurpose"/>.
   /// </summary>
   /// <param name="value">The purpose value to check.</param>
   /// <returns>
   /// <see langword="true"/> if the given <paramref name="value"/> has a purpose flag set,
   /// that is not <see cref="SeverityAndPurpose.NoPurpose"/>, <see langword="false"/> otherwise.
   /// </returns>
   public static bool HasPurpose(this SeverityAndPurpose value)
      => IsPurposeSet(value) && !value.HasFlag(SeverityAndPurpose.NoPurpose);
   #endregion

   #region Functions
   /// <summary>Gets all the possible purpose flags.</summary>
   /// <returns>An enumerable of all the possible purpose flags.</returns>
   public static IEnumerable<SeverityAndPurpose> GetAll()
   {
      foreach (SeverityAndPurpose value in Enum.GetValues(typeof(SeverityAndPurpose)))
      {
         bool hasPurpose = value.HasPurpose();
         bool noSeverity = value.HasSeverity() == false;
         bool notInherited = value != SeverityAndPurpose.InheritPurpose;
            if (hasPurpose && noSeverity && notInherited)
            yield return value;
      }
   }
   #endregion
}
