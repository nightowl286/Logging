using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Entries.Components.Tag;

namespace TNO.Logging.Reading.Entries.Components.Tag.Versions;

/// <summary>
/// A deserialiser for <see cref="ITagComponent"/>, version #0.
/// </summary>
[Version(0)]
public sealed class TagComponentDeserialiser0 : ITagComponentDeserialiser
{
   #region Methods
   /// <inheritdoc/>
   public ITagComponent Deserialise(BinaryReader reader)
   {
      ulong tagId = reader.ReadUInt64();

      return TagComponentFactory.Version0(tagId);
   }
   #endregion
}