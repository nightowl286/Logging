using TNO.Common.Abstractions.Components;
using TNO.Common.Abstractions.Components.Kinds;

namespace TNO.Logging.Writer.Entries.Components.Kinds;

internal record TagComponent(ulong TagId) : IEntryComponent, ITagComponent
{
   #region Properties
   public ComponentKind Kind => ComponentKind.Tag;
   #endregion
}
