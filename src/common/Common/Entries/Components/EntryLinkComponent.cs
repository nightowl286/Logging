using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.EntryLink"/> component.
/// </summary>
public class EntryLinkComponent : IEntryLinkComponent
{
   #region Properties
   /// <inheritdoc/>
   public ulong EntryId { get; }

   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.EntryLink;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="EntryLinkComponent"/>.</summary>
   /// <param name="entryId">The linked <see cref="IEntry.Id"/>.</param>
   public EntryLinkComponent(ulong entryId)
   {
      EntryId = entryId;
   }
   #endregion
}
