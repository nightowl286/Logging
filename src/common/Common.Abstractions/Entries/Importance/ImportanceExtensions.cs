namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// Contains useful extension methods related to the <see cref="Importance"/>.
/// </summary>
public static class ImportanceExtensions
{
   #region Methods
   /// <summary>
   /// Checks whether the given <paramref name="value"/> is logically
   /// equivalent to <see cref="Importance.None"/>.
   /// </summary>
   /// <param name="value">The value to check.</param>
   /// <returns>
   /// <see langword="true"/> if the given <paramref name="value"/> was logically
   /// equivalent to <see cref="Importance.None"/>, <see langword="false"/> otherwise.
   /// </returns>
   public static bool IsNone(this Importance value)
   {
      switch (value)
      {
         case Importance.Empty:
         case Importance.None:
         case Importance.NoPurpose:
         case Importance.NoSeverity:
            return true;

         default:
            return false;
      }
   }

   /// <summary>Normalises the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to normalise.</param>
   /// <remarks>Normalised values always have the severity and the purpose set.</remarks>
   public static void Normalise(ref this Importance value)
   {
      Importance normalised = value.Normalised();
      value = normalised;
   }

   /// <summary>Gets the normalised variant of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to normalise.</param>
   /// <returns>The normalised value.</returns>
   /// <remarks>Normalised values always have the severity and the purpose set.</remarks>
   public static Importance Normalised(this Importance value)
   {
      if (value.IsPurposeSet() == false)
         value |= Importance.NoPurpose;

      if (value.IsSeveritySet() == false)
         value |= Importance.NoSeverity;

      return value;
   }
   #endregion
}