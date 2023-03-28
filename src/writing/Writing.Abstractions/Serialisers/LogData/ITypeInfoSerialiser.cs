using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers.LogData;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.TypeInfo)]
public interface ITypeInfoSerialiser : IBinarySerialiser<ITypeInfo>, IVersioned
{
}