using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;
/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.TagReference)]
public interface ITagReferenceSerialiser : IBinarySerialiser<TagReference>, IVersioned
{
}