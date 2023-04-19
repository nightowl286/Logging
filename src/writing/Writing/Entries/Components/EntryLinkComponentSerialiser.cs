using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="IEntryLinkComponent"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.EntryLink)]
public sealed class EntryLinkComponentSerialiser : ISerialiser<IEntryLinkComponent>
{

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IEntryLinkComponent data)
   {
      ulong id = data.EntryId;

      writer.Write(id);
   }

   /// <inheritdoc/>
   public ulong Count(IEntryLinkComponent data) => sizeof(ulong);
   #endregion
}
