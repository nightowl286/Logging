using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Abstractions.Entries;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.Entry)]
public interface IEntrySerialiser : IBinarySerialiser<IEntry>, IVersioned
{
}