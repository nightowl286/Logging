namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// Contains useful extension methods related to <see cref="Purpose"/>.
/// </summary>
public static class PurposeExtensions
{
   #region Methods
   /// <summary>Checks whether the given <paramref name="value"/> has a flag set for the purpose.</summary>
   /// <param name="value">The purpose value to check.</param>
   /// <returns><see langword="true"/> if a purpose flag is set, <see langword="false"/> otherwise.</returns>
   public static bool IsPurposeSet(this Importance value)
      => value.GetSetPurpose() != Importance.Empty;

   /// <summary>Gets the purpose flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the purpose flag of.</param>
   /// <returns>
   /// The purpose flag that was set on the given <paramref name="value"/>,
   /// or <see cref="Importance.Empty"/> if no purpose flag wasn't set.
   /// </returns>
   public static Importance GetSetPurpose(this Importance value)
   {
      byte masked = (byte)((byte)value & Purpose.BitMask);

      return (Importance)masked;
   }

   /// <summary>Gets the purpose flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the purpose flag of.</param>
   /// <returns>
   /// The purpose flag that was set on the given <paramref name="value"/>,
   /// or <see cref="Importance.NoPurpose"/> if no purpose flag wasn't set.
   /// </returns>
   public static Importance GetPurpose(this Importance value)
   {
      value = value.GetSetPurpose();

      return value == Importance.Empty ? Importance.NoPurpose : value;
   }

   /// <summary>
   /// Checks whether the given <paramref name="value"/> has a flag set 
   /// for purpose, and it is not <see cref="Importance.NoPurpose"/>.
   /// </summary>
   /// <param name="value">The purpose value to check.</param>
   /// <returns>
   /// <see langword="true"/> if the given <paramref name="value"/> has a purpose flag set,
   /// that is not <see cref="Importance.NoPurpose"/>, <see langword="false"/> otherwise.
   /// </returns>
   public static bool HasPurpose(this Importance value)
   {
      return
         value.IsPurposeSet() &&
         value != Importance.NoPurpose &&
         value != Importance.None;
   }
   #endregion
}