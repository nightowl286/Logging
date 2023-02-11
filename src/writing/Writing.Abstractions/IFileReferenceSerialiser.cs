using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Abstractions;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.FileReference)]
public interface IFileReferenceSerialiser : IBinarySerialiser<FileReference>, IVersioned
{
}