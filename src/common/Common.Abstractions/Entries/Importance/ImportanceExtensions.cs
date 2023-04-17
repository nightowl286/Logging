namespace TNO.Logging.Common.Abstractions.Entries.Importance;

/// <summary>
/// Contains useful extension methods related to the <see cref="ImportanceCombination"/>.
/// </summary>
public static class ImportanceExtensions
{
   #region Methods
   /// <summary>
   /// Checks whether the given <paramref name="value"/> is logically
   /// equivalent to <see cref="ImportanceCombination.None"/>.
   /// </summary>
   /// <param name="value">The value to check.</param>
   /// <returns>
   /// <see langword="true"/> if the given <paramref name="value"/> was logically
   /// equivalent to <see cref="ImportanceCombination.None"/>, <see langword="false"/> otherwise.
   /// </returns>
   public static bool IsNone(this ImportanceCombination value)
   {
      switch (value)
      {
         case ImportanceCombination.Empty:
         case ImportanceCombination.None:
         case ImportanceCombination.NoPurpose:
         case ImportanceCombination.NoSeverity:
            return true;

         default:
            return false;
      }
   }

   /// <summary>Normalises the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to normalise.</param>
   /// <remarks>Normalised values always have the severity and the purpose set.</remarks>
   public static void Normalise(ref this ImportanceCombination value)
   {
      ImportanceCombination normalised = value.Normalised();
      value = normalised;
   }

   /// <summary>Gets the normalised variant of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to normalise.</param>
   /// <returns>The normalised value.</returns>
   /// <remarks>Normalised values always have the severity and the purpose set.</remarks>
   public static ImportanceCombination Normalised(this ImportanceCombination value)
   {
      if (value.IsPurposeSet() == false)
         value |= ImportanceCombination.NoPurpose;

      if (value.IsSeveritySet() == false)
         value |= ImportanceCombination.NoSeverity;

      return value;
   }
   #endregion
}