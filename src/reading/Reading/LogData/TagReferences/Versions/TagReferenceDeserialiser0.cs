using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.TagReferences.Versions;

/// <summary>
/// A deserialiser for <see cref="TagReference"/>, version #0.
/// </summary>
[Version(0)]
public sealed class TagReferenceDeserialiser0 : IDeserialiser<TagReference>
{
   #region Methods
   /// <inheritdoc/>
   public TagReference Deserialise(BinaryReader reader)
   {
      string tag = reader.ReadString();
      ulong id = reader.ReadUInt64();

      return TagReferenceFactory.Version0(tag, id);
   }
   #endregion
}