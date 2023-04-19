using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;

namespace TNO.Logging.Reading.Entries.Components.EntryLink;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="IEntryLinkComponent"/>.
/// </summary>
internal static class EntryLinkComponentFactory
{
   #region Functions
   public static IEntryLinkComponent Version0(ulong entryId) => new EntryLinkComponent(entryId);
   #endregion
}