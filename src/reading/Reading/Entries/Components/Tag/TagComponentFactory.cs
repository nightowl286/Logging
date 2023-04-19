using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;

namespace TNO.Logging.Reading.Entries.Components.Tag;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="ITagComponent"/>.
/// </summary>
internal static class TagComponentFactory
{
   #region Functions
   public static ITagComponent Version0(ulong tagId) => new TagComponent(tagId);
   #endregion
}