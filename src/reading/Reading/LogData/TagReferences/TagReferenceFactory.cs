using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Reading.Abstractions.LogData.TagReferences;

namespace TNO.Logging.Reading.LogData.TagReferences;

/// <summary>
/// A factory that should be used in instances of the <see cref="ITagReferenceDeserialiser"/>.
/// </summary>
internal static class TagReferenceFactory
{
   #region Functions
   public static TagReference Version0(string tag, ulong id)
      => new TagReference(tag, id);
   #endregion
}
