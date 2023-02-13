using System;
using System.Collections.Generic;

namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// A helper class for creating the purpose component of the <see cref="Importance"/> enum.
/// </summary>
public static class Purpose
{
   #region Consts
   /// <summary>The bit mask used for the purpose component of the <see cref="Importance"/> enum.</summary>
   public const byte BitMask = 0b1111_0000;
   #endregion

   #region Properties
   /// <inheritdoc cref="Importance.Telemetry"/>
   public static Importance Telemetry { get; } = Importance.Telemetry;

   /// <inheritdoc cref="Importance.Tracing"/>
   public static Importance Tracing { get; } = Importance.Tracing;

   /// <inheritdoc cref="Importance.Diagnostics"/>
   public static Importance Diagnostics { get; } = Importance.Diagnostics;

   /// <inheritdoc cref="Importance.Performance"/>
   public static Importance Performance { get; } = Importance.Performance;
   #endregion

   #region Methods
   /// <summary>Checks whether the given <paramref name="value"/> has a flag set for the purpose.</summary>
   /// <param name="value">The purpose value to check.</param>
   /// <returns><see langword="true"/> if a purpose flag is set, <see langword="false"/> otherwise.</returns>
   public static bool IsPurposeSet(this Importance value)
      => value.GetSetPurpose() != Importance.Empty;

   /// <summary>Gets the purpose flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the purpose flag of.</param>
   /// <returns>
   /// The purpose flag that was set on the given <paramref name="value"/>,
   /// or <see cref="Importance.Empty"/> if no purpose flag wasn't set.
   /// </returns>
   public static Importance GetSetPurpose(this Importance value)
   {
      byte masked = (byte)((byte)value & BitMask);

      return (Importance)masked;
   }

   /// <summary>Gets the purpose flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the purpose flag of.</param>
   /// <returns>
   /// The purpose flag that was set on the given <paramref name="value"/>,
   /// or <see cref="Importance.NoPurpose"/> if no purpose flag wasn't set.
   /// </returns>
   public static Importance GetPurpose(this Importance value)
   {
      value = value.GetSetPurpose();

      return value == Importance.Empty ? Importance.NoPurpose : value;
   }

   /// <summary>
   /// Checks whether the given <paramref name="value"/> has a flag set 
   /// for purpose, and it is not <see cref="Importance.NoPurpose"/>.
   /// </summary>
   /// <param name="value">The purpose value to check.</param>
   /// <returns>
   /// <see langword="true"/> if the given <paramref name="value"/> has a purpose flag set,
   /// that is not <see cref="Importance.NoPurpose"/>, <see langword="false"/> otherwise.
   /// </returns>
   public static bool HasPurpose(this Importance value)
   {
      return
         value.IsPurposeSet() &&
         value != Importance.NoPurpose &&
         value != Importance.None;
   }
   #endregion

   #region Functions
   /// <summary>Gets all the possible purpose flags.</summary>
   /// <returns>An enumerable of all the possible purpose flags.</returns>
   public static IEnumerable<Importance> GetAll()
   {
      foreach (Importance value in Enum.GetValues(typeof(Importance)))
      {
         bool hasPurpose = value.HasPurpose();
         bool noSeverity = value.HasSeverity() == false;
         bool notInherited = value != Importance.InheritPurpose;

         if (hasPurpose && noSeverity && notInherited)
            yield return value;
      }
   }
   #endregion
}
