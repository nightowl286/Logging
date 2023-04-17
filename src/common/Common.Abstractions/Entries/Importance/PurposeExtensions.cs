/* Unmerged change from project 'Common.Abstractions (netstandard2.1)'
Before:
namespace TNO.Logging.Common.Abstractions.Entries.Importance;
After:
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.Entries.Importance.Importance;

namespace TNO.Logging.Common.Abstractions.Entries.Importance.Importance.Importance;
*/


/* Unmerged change from project 'Common.Abstractions (netstandard2.1)'
Before:
namespace TNO.Logging.Common.Abstractions.Entries.Importance.Importance;
After:
using TNO;
using TNO.Logging;
using TNO.Logging.Common;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.Entries.Importance.Importance;
*/

namespace TNO.Logging.Common.Abstractions.Entries.Importance;

/// <summary>
/// Contains useful extension methods related to <see cref="Purpose"/>.
/// </summary>
public static class PurposeExtensions
{
   #region Methods
   /// <summary>Checks whether the given <paramref name="value"/> has a flag set for the purpose.</summary>
   /// <param name="value">The purpose value to check.</param>
   /// <returns><see langword="true"/> if a purpose flag is set, <see langword="false"/> otherwise.</returns>
   public static bool IsPurposeSet(this ImportanceCombination value)
      => value.GetSetPurpose() != ImportanceCombination.Empty;

   /// <summary>Gets the purpose flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the purpose flag of.</param>
   /// <returns>
   /// The purpose flag that was set on the given <paramref name="value"/>,
   /// or <see cref="ImportanceCombination.Empty"/> if no purpose flag wasn't set.
   /// </returns>
   public static ImportanceCombination GetSetPurpose(this ImportanceCombination value)
   {
      byte masked = (byte)((byte)value & Purpose.BitMask);

      return (ImportanceCombination)masked;
   }

   /// <summary>Gets the purpose flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the purpose flag of.</param>
   /// <returns>
   /// The purpose flag that was set on the given <paramref name="value"/>,
   /// or <see cref="ImportanceCombination.NoPurpose"/> if no purpose flag wasn't set.
   /// </returns>
   public static ImportanceCombination GetPurpose(this ImportanceCombination value)
   {
      value = value.GetSetPurpose();

      return value == ImportanceCombination.Empty ? ImportanceCombination.NoPurpose : value;
   }

   /// <summary>
   /// Checks whether the given <paramref name="value"/> has a flag set 
   /// for purpose, and it is not <see cref="ImportanceCombination.NoPurpose"/>.
   /// </summary>
   /// <param name="value">The purpose value to check.</param>
   /// <returns>
   /// <see langword="true"/> if the given <paramref name="value"/> has a purpose flag set,
   /// that is not <see cref="ImportanceCombination.NoPurpose"/>, <see langword="false"/> otherwise.
   /// </returns>
   public static bool HasPurpose(this ImportanceCombination value)
   {
      return
         value.IsPurposeSet() &&
         value != ImportanceCombination.NoPurpose &&
         value != ImportanceCombination.None;
   }
   #endregion
}