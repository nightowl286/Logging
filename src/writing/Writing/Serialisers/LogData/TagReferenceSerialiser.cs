using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData;

namespace TNO.Logging.Writing.Serialisers.LogData;

/// <summary>
/// A serialiser for <see cref="TagReference"/>.
/// </summary>
[Version(0)]
public class TagReferenceSerialiser : ITagReferenceSerialiser
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, TagReference data)
   {
      string tag = data.Tag;
      ulong id = data.Id;

      writer.Write(tag);
      writer.Write(id);
   }

   /// <inheritdoc/>
   public ulong Count(TagReference data)
   {
      string tag = data.Tag;

      int tagSize = BinaryWriterSizeHelper.StringSize(tag);

      return (ulong)(tagSize + sizeof(ulong));
   }
   #endregion
}