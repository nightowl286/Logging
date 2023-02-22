using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Reading.Abstractions.LogData.TagReferences;

namespace TNO.Logging.Reading.LogData.TagReferences.Versions;

/// <summary>
/// A deserialiser for <see cref="TagReference"/>, version #0.
/// </summary>
public sealed class TagReferenceDeserialiser0 : ITagReferenceDeserialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

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