using System;
using System.Collections.Generic;
using TNO.Logging.Common.Abstractions.Entries;

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

   #region Methods
   /// <summary>Checks whether the given <paramref name="value"/> has a flag set for the severity.</summary>
   /// <param name="value">The severity value to check.</param>
   /// <returns><see langword="true"/> if a severity flag is set, <see langword="false"/> otherwise.</returns>
   public static bool IsSeveritySet(this SeverityAndPurpose value)
      => GetSetSeverity(value) != SeverityAndPurpose.Empty;

   /// <summary>Gets the severity flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the severity flag of.</param>
   /// <returns>
   /// The severity flag that was set on the given <paramref name="value"/>,
   /// or <see cref="SeverityAndPurpose.Empty"/> if no severity flag wasn't set.
   /// </returns>
   public static SeverityAndPurpose GetSetSeverity(this SeverityAndPurpose value)
   {
      byte masked = (byte)(((byte)value) & BitMask);

      return (SeverityAndPurpose)masked;
   }

   /// <summary>Gets the severity flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the severity flag of.</param>
   /// <returns>
   /// The severity flag that was set on the given <paramref name="value"/>,
   /// or <see cref="SeverityAndPurpose.NoSeverity"/> if no severity flag wasn't set.
   /// </returns>
   public static SeverityAndPurpose GetSeverity(this SeverityAndPurpose value)
   {
      value = GetSetSeverity(value);

      return value == SeverityAndPurpose.Empty ? SeverityAndPurpose.NoSeverity : value;
   }

   /// <summary>
   /// Checks whether the given <paramref name="value"/> has a flag set 
   /// for severity, and it is not <see cref="SeverityAndPurpose.NoSeverity"/>.
   /// </summary>
   /// <param name="value">The severity value to check.</param>
   /// <returns>
   /// <see langword="true"/> if the given <paramref name="value"/> has a severity flag set,
   /// that is not <see cref="SeverityAndPurpose.NoSeverity"/>, <see langword="false"/> otherwise.
   /// </returns>
   public static bool HasSeverity(this SeverityAndPurpose value)
   { 
      return 
         IsSeveritySet(value) && 
         value != SeverityAndPurpose.NoSeverity &&
         value != SeverityAndPurpose.None;
   }
   #endregion

   #region Functions
   /// <summary>Gets all the possible severity flags.</summary>
   /// <returns>An enumerable of all the possible severity flags.</returns>
   public static IEnumerable<SeverityAndPurpose> GetAll()
   {
      foreach (SeverityAndPurpose value in Enum.GetValues(typeof(SeverityAndPurpose)))
      {
         bool hasSeverity = value.HasSeverity();
         bool noPurpose = value.HasPurpose() == false;
         bool notInherited = value != SeverityAndPurpose.InheritSeverity;

         if (hasSeverity && noPurpose && notInherited)
            yield return value;
      }
   }
   #endregion
}
