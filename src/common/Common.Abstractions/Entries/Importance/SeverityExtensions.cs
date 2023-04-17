/* Unmerged change from project 'Common.Abstractions (netstandard2.1)'
Before:
namespace TNO.Logging.Common.Abstractions.Entries;
After:
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.Entries.Importance.Importance;

namespace TNO.Logging.Common.Abstractions.Entries;
*/

namespace TNO.Logging.Common.Abstractions.Entries.Importance;

/// <summary>
/// Contains helpful extension methods related to <see cref="Severity"/>.
/// </summary>
public static class SeverityExtensions
{
   #region Methods
   /// <summary>Checks whether the given <paramref name="value"/> has a flag set for the severity.</summary>
   /// <param name="value">The severity value to check.</param>
   /// <returns><see langword="true"/> if a severity flag is set, <see langword="false"/> otherwise.</returns>
   public static bool IsSeveritySet(this ImportanceCombination value)
      => value.GetSetSeverity() != ImportanceCombination.Empty;

   /// <summary>Gets the severity flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the severity flag of.</param>
   /// <returns>
   /// The severity flag that was set on the given <paramref name="value"/>,
   /// or <see cref="ImportanceCombination.Empty"/> if no severity flag wasn't set.
   /// </returns>
   public static ImportanceCombination GetSetSeverity(this ImportanceCombination value)
   {
      byte masked = (byte)((byte)value & Severity.BitMask);

      return (ImportanceCombination)masked;
   }

   /// <summary>Gets the severity flag of the given <paramref name="value"/>.</summary>
   /// <param name="value">The value to get the severity flag of.</param>
   /// <returns>
   /// The severity flag that was set on the given <paramref name="value"/>,
   /// or <see cref="ImportanceCombination.NoSeverity"/> if no severity flag wasn't set.
   /// </returns>
   public static ImportanceCombination GetSeverity(this ImportanceCombination value)
   {
      value = value.GetSetSeverity();

      return value == ImportanceCombination.Empty ? ImportanceCombination.NoSeverity : value;
   }

   /// <summary>
   /// Checks whether the given <paramref name="value"/> has a flag set 
   /// for severity, and it is not <see cref="ImportanceCombination.NoSeverity"/>.
   /// </summary>
   /// <param name="value">The severity value to check.</param>
   /// <returns>
   /// <see langword="true"/> if the given <paramref name="value"/> has a severity flag set,
   /// that is not <see cref="ImportanceCombination.NoSeverity"/>, <see langword="false"/> otherwise.
   /// </returns>
   public static bool HasSeverity(this ImportanceCombination value)
   {
      return
         value.IsSeveritySet() &&
         value != ImportanceCombination.NoSeverity &&
         value != ImportanceCombination.None;
   }
   #endregion
}