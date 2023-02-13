namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// Contains helpful extension methods related to <see cref="Severity"/>.
/// </summary>
public static class SeverityExtensions
{
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
      byte masked = (byte)((byte)value & Severity.BitMask);

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
}
