using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Entries.Components.EntryLink.Versions;

/// <summary>
/// A deserialiser for <see cref="IEntryLinkComponent"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.EntryLink)]
public sealed class EntryLinkComponentDeserialiser0 : IDeserialiser<IEntryLinkComponent>
{
   #region Methods
   /// <inheritdoc/>
   public IEntryLinkComponent Deserialise(BinaryReader reader)
   {
      ulong entryId = reader.ReadUInt64();

      return EntryLinkComponentFactory.Version0(entryId);
   }
   #endregion
}