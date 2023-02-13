using System;
using System.Collections.Generic;

namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// A helper class for creating the severity component of the <see cref="Importance"/> enum.
/// </summary>
public readonly struct Severity
{
   #region Consts
   /// <summary>The bit mask used for the severity component of the <see cref="Importance"/> enum.</summary>
   public const byte BitMask = 0b0000_1111;
   #endregion

   #region Fields
   /// <summary>The <see cref="Importance"/> value that this severity represents.</summary>
   public readonly Importance Value;
   #endregion

   #region Properties
   /// <inheritdoc cref="Importance.Negligible"/>
   public static Severity Negligible { get; } = new Severity(Importance.Negligible);

   /// <inheritdoc cref="Importance.Substantial"/>
   public static Severity Substantial { get; } = new Severity(Importance.Substantial);

   /// <inheritdoc cref="Importance.Critical"/>
   public static Severity Critical { get; } = new Severity(Importance.Critical);

   /// <inheritdoc cref="Importance.Fatal"/>
   public static Severity Fatal { get; } = new Severity(Importance.Fatal);
   #endregion
   private Severity(Importance severity) => Value = severity;

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

   #region Operators
   /// <summary>Combines the given <paramref name="severity"/> and <paramref name="purpose"/> into an <see cref="Importance"/> value.</summary>
   /// <param name="severity">The severity value.</param>
   /// <param name="purpose">The purpose value.</param>
   /// <returns>The combined importance value.</returns>
   public static Importance operator |(Severity severity, Purpose purpose) => severity.Value | purpose.Value;
   #endregion
}