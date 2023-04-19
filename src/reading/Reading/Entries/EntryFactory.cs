using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Entries;

namespace TNO.Logging.Reading.Entries;

/// <summary>
/// A factory that should be used in instances of the <see cref="IEntry"/>.
/// </summary>
internal static class EntryFactory
{
   #region Functions
   public static IEntry Version0(
      ulong id,
      ulong contextId,
      ulong scope,
      ImportanceCombination Importance,
      TimeSpan timestamp,
      ulong fileId,
      uint line,
      IReadOnlyDictionary<ComponentKind, IComponent> components)
   {
      return new Entry(
         id,
         contextId,
         scope,
         Importance,
         timestamp,
         fileId,
         line,
         components);
   }
   #endregion
}