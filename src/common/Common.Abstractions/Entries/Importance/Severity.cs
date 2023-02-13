using System;
using System.Collections.Generic;

namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// A helper class for creating the severity component of the <see cref="Importance"/> enum.
/// </summary>
public static class Severity
{
   #region Consts
   /// <summary>The bit mask used for the severity component of the <see cref="Importance"/> enum.</summary>
   public const byte BitMask = 0b0000_1111;
   #endregion

   #region Properties
   /// <inheritdoc cref="Importance.Negligible"/>
   public static Importance Negligible { get; } = Importance.Negligible;

   /// <inheritdoc cref="Importance.Substantial"/>
   public static Importance Substantial { get; } = Importance.Substantial;

   /// <inheritdoc cref="Importance.Critical"/>
   public static Importance Critical { get; } = Importance.Critical;

   /// <inheritdoc cref="Importance.Fatal"/>
   public static Importance Fatal { get; } = Importance.Fatal;
   #endregion

   #region Methods
   /// <summary>Checks whether the given <paramref name="value"/> has a flag set for the severity.</summary>
   /// <param name="value">The severity value to check.</param>
   /// <returns><see langword="true"/> if a severity flag is set, <see langword="false"/> otherwise.</returns>
   public static bool IsSeveritySet(this Importance value)
      => value.GetSetSeverity() != Importance.Empty;

   /// <summary>Gets the severity flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the severity flag of.</param>
   /// <returns>
   /// The severity flag that was set on the given <paramref name="value"/>,
   /// or <see cref="Importance.Empty"/> if no severity flag wasn't set.
   /// </returns>
   public static Importance GetSetSeverity(this Importance value)
   {
      byte masked = (byte)((byte)value & BitMask);

      return (Importance)masked;
   }

   /// <summary>Gets the severity flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the severity flag of.</param>
   /// <returns>
   /// The severity flag that was set on the given <paramref name="value"/>,
   /// or <see cref="Importance.NoSeverity"/> if no severity flag wasn't set.
   /// </returns>
   public static Importance GetSeverity(this Importance value)
   {
      value = value.GetSetSeverity();

      return value == Importance.Empty ? Importance.NoSeverity : value;
   }

   /// <summary>
   /// Checks whether the given <paramref name="value"/> has a flag set 
   /// for severity, and it is not <see cref="Importance.NoSeverity"/>.
   /// </summary>
   /// <param name="value">The severity value to check.</param>
   /// <returns>
   /// <see langword="true"/> if the given <paramref name="value"/> has a severity flag set,
   /// that is not <see cref="Importance.NoSeverity"/>, <see langword="false"/> otherwise.
   /// </returns>
   public static bool HasSeverity(this Importance value)
   {
      return
         value.IsSeveritySet() &&
         value != Importance.NoSeverity &&
         value != Importance.None;
   }
   #endregion

   #region Functions
   /// <summary>Gets all the possible severity flags.</summary>
   /// <returns>An enumerable of all the possible severity flags.</returns>
   public static IEnumerable<Importance> GetAll()
   {
      foreach (Importance value in Enum.GetValues(typeof(Importance)))
      {
         bool hasSeverity = value.HasSeverity();
         bool noPurpose = value.HasPurpose() == false;
         bool notInherited = value != Importance.InheritSeverity;

         if (hasSeverity && noPurpose && notInherited)
            yield return value;
      }
   }
   #endregion
}
