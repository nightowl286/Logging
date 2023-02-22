using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Tag;

namespace TNO.Logging.Reading.Entries.Components.Tag;

/// <summary>
/// A factory class that should be used instances of the <see cref="ITagComponentDeserialiser"/>.
/// </summary>
internal static class TagComponentFactory
{
   #region Functions
   public static ITagComponent Version0(ulong tagId) => new TagComponent(tagId);
   #endregion
}