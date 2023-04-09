using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers.LogData.Constructors;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.ConstructorInfo)]
public interface IConstructorInfoSerialiser : IBinarySerialiser<IConstructorInfo>, IVersioned
{
}