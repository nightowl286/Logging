using TNO.Common.Abstractions;

namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// Contains useful extension methods related to the <see cref="SeverityAndPurpose"/>.
/// </summary>
public static class SeverityAndPurposeExtensions
{
   #region Methods
   /// <summary>
   /// Checks whether the given <paramref name="value"/> is logically
   /// equivalent to <see cref="SeverityAndPurpose.None"/>.
   /// </summary>
   /// <param name="value">The value to check.</param>
   /// <returns>
   /// <see langword="true"/> if the given <paramref name="value"/> was logically
   /// equivalent to <see cref="SeverityAndPurpose.None"/>, <see langword="false"/> otherwise.
   /// </returns>
   public static bool IsNone(this SeverityAndPurpose value)
   {
      switch (value)
      {
         case SeverityAndPurpose.Empty:
         case SeverityAndPurpose.None:
         case SeverityAndPurpose.NoPurpose:
         case SeverityAndPurpose.NoSeverity:
            return true;

         default:
            return false;
      }
   }

   /// <summary>Normalises the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to normalise.</param>
   /// <remarks>Normalised values always have the severity and the purpose set.</remarks>
   public static void Normalise(ref this SeverityAndPurpose value)
   {
      SeverityAndPurpose normalised = Normalised(value);
      value = normalised;
   }

   /// <summary>Gets the normalised variant of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to normalise.</param>
   /// <returns>The normalised value.</returns>
   /// <remarks>Normalised values always have the severity and the purpose set.</remarks>
   public static SeverityAndPurpose Normalised(this SeverityAndPurpose value)
   {
      if (value.IsPurposeSet() == false)
         value |= SeverityAndPurpose.NoPurpose;

      if (value.IsSeveritySet() == false)
         value |= SeverityAndPurpose.NoSeverity;

      return value;
   }
   #endregion
}
