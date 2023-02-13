using System;
using System.Collections.Generic;

namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// A helper class for creating the purpose component of the <see cref="Importance"/> enum.
/// </summary>
public readonly struct Purpose
{
   #region Consts
   /// <summary>The bit mask used for the purpose component of the <see cref="Importance"/> enum.</summary>
   public const byte BitMask = 0b1111_0000;
   #endregion

   #region Fields
   /// <summary>The <see cref="Importance"/> value that this purpose represents.</summary>
   public readonly Importance Value;
   #endregion

   #region Properties
   /// <inheritdoc cref="Importance.Telemetry"/>
   public static Purpose Telemetry { get; } = new Purpose(Importance.Telemetry);

   /// <inheritdoc cref="Importance.Tracing"/>
   public static Purpose Tracing { get; } = new Purpose(Importance.Tracing);

   /// <inheritdoc cref="Importance.Diagnostics"/>
   public static Purpose Diagnostics { get; } = new Purpose(Importance.Diagnostics);

   /// <inheritdoc cref="Importance.Performance"/>
   public static Purpose Performance { get; } = new Purpose(Importance.Performance);
   #endregion
   private Purpose(Importance purpose) => Value = purpose;

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
