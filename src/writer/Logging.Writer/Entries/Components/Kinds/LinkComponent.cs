using TNO.Common.Abstractions.Components;
using TNO.Common.Abstractions.Components.Kinds;

namespace TNO.Logging.Writer.Entries.Components.Kinds;

internal record LinkComponent(ulong IdOfLinkedEntry) : IEntryComponent, ILinkComponent
{
   #region Properties
   public ComponentKind Kind => ComponentKind.EntryLink;
   #endregion
}
