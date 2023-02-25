using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.EntryLink"/> component.
/// </summary>
/// <param name="EntryId">The linked <see cref="IEntry.Id"/>.</param>
public record class EntryLinkComponent(ulong EntryId) : IEntryLinkComponent
{
   #region Properties
   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.EntryLink;
   #endregion
}
