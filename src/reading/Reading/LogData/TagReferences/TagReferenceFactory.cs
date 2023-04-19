using TNO.Logging.Common.Abstractions.LogData;

namespace TNO.Logging.Reading.LogData.TagReferences;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="TagReference"/>.
/// </summary>
internal static class TagReferenceFactory
{
   #region Functions
   public static TagReference Version0(string tag, ulong id)
      => new TagReference(tag, id);
   #endregion
}
