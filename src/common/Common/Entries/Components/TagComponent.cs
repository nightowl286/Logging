using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Tag"/> component.
/// </summary>
/// <param name="TagId">The id of the referenced tag.</param>
public record class TagComponent(ulong TagId) : ITagComponent
{
   #region Properties
   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Tag;
   #endregion
}
