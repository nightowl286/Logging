using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="ITagComponent"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.Tag)]
public sealed class TagComponentSerialiser : ISerialiser<ITagComponent>
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ITagComponent data)
   {
      ulong id = data.TagId;
      writer.Write(id);
   }

   /// <inheritdoc/>
   public int Count(ITagComponent data) => sizeof(ulong);
   #endregion
}