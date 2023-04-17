using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Entries.Components;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.Type)]
public interface ITypeComponentSerialiser : IBinarySerialiser<ITypeComponent>, IVersioned
{
}
