using System;
using System.Collections.Generic;

namespace TNO.Logging.Common.Abstractions.Entries.Importance;

/// <summary>
/// A helper class for creating the severity component of the <see cref="ImportanceCombination"/> enum.
/// </summary>
public readonly struct Severity
{
   #region Consts
   /// <summary>The bit mask used for the severity component of the <see cref="ImportanceCombination"/> enum.</summary>
   public const byte BitMask = 0b0000_1111;
   #endregion

   #region Fields
   /// <summary>The <see cref="ImportanceCombination"/> value that this severity represents.</summary>
   public readonly ImportanceCombination Value;
   #endregion

   #region Properties
   /// <inheritdoc cref="ImportanceCombination.Negligible"/>
   public static Severity Negligible { get; } = new Severity(ImportanceCombination.Negligible);

   /// <inheritdoc cref="ImportanceCombination.Substantial"/>
   public static Severity Substantial { get; } = new Severity(ImportanceCombination.Substantial);

   /// <inheritdoc cref="ImportanceCombination.Critical"/>
   public static Severity Critical { get; } = new Severity(ImportanceCombination.Critical);

   /// <inheritdoc cref="ImportanceCombination.Fatal"/>
   public static Severity Fatal { get; } = new Severity(ImportanceCombination.Fatal);
   #endregion
   private Severity(ImportanceCombination severity) => Value = severity;

   #region Functions
   /// <summary>Gets all the possible severity flags.</summary>
   /// <returns>An enumerable of all the possible severity flags.</returns>
   public static IEnumerable<ImportanceCombination> GetAll()
   {
      foreach (ImportanceCombination value in Enum.GetValues(typeof(ImportanceCombination)))
      {
         bool hasSeverity = value.HasSeverity();
         bool noPurpose = value.HasPurpose() == false;
         bool notInherited = value != ImportanceCombination.InheritSeverity;

         if (hasSeverity && noPurpose && notInherited)
            yield return value;
      }
   }
   #endregion

   #region Operators
   /// <summary>Combines the given <paramref name="severity"/> and <paramref name="purpose"/> into an <see cref="ImportanceCombination"/> value.</summary>
   /// <param name="severity">The severity value.</param>
   /// <param name="purpose">The purpose value.</param>
   /// <returns>The combined importance value.</returns>
   public static ImportanceCombination operator |(Severity severity, Purpose purpose) => severity.Value | purpose.Value;
   #endregion
}