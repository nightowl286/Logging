using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Entries;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.Entry)]
public interface IEntrySerialiser : IBinarySerialiser<IEntry>, IVersioned
{
}