using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Tag"/> component.
/// </summary>
public class TagComponent : ITagComponent
{
   #region Properties
   /// <inheritdoc/>
   public ulong TagId { get; }

   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Tag;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TagComponent"/>.</summary>
   /// <param name="tagId">The id of the referenced tag.</param>
   public TagComponent(ulong tagId)
   {
      TagId = tagId;
   }
   #endregion
}
