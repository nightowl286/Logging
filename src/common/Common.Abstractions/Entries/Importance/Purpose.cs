using System;
using System.Collections.Generic;

namespace TNO.Logging.Common.Abstractions.Entries.Importance;

/// <summary>
/// A helper class for creating the purpose component of the <see cref="ImportanceCombination"/> enum.
/// </summary>
public readonly struct Purpose
{
   #region Consts
   /// <summary>The bit mask used for the purpose component of the <see cref="ImportanceCombination"/> enum.</summary>
   public const byte BitMask = 0b1111_0000;
   #endregion

   #region Fields
   /// <summary>The <see cref="ImportanceCombination"/> value that this purpose represents.</summary>
   public readonly ImportanceCombination Value;
   #endregion

   #region Properties
   /// <inheritdoc cref="ImportanceCombination.Telemetry"/>
   public static Purpose Telemetry { get; } = new Purpose(ImportanceCombination.Telemetry);

   /// <inheritdoc cref="ImportanceCombination.Tracing"/>
   public static Purpose Tracing { get; } = new Purpose(ImportanceCombination.Tracing);

   /// <inheritdoc cref="ImportanceCombination.Diagnostics"/>
   public static Purpose Diagnostics { get; } = new Purpose(ImportanceCombination.Diagnostics);

   /// <inheritdoc cref="ImportanceCombination.Performance"/>
   public static Purpose Performance { get; } = new Purpose(ImportanceCombination.Performance);
   #endregion
   private Purpose(ImportanceCombination purpose) => Value = purpose;

   #region Functions
   /// <summary>Gets all the possible purpose flags.</summary>
   /// <returns>An enumerable of all the possible purpose flags.</returns>
   public static IEnumerable<ImportanceCombination> GetAll()
   {
      foreach (ImportanceCombination value in Enum.GetValues(typeof(ImportanceCombination)))
      {
         bool hasPurpose = value.HasPurpose();
         bool noSeverity = value.HasSeverity() == false;
         bool notInherited = value != ImportanceCombination.InheritPurpose;

         if (hasPurpose && noSeverity && notInherited)
            yield return value;
      }
   }
   #endregion
}