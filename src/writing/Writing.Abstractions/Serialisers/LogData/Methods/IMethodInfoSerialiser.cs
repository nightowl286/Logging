using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers.LogData.Methods;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.MethodInfo)]
public interface IMethodInfoSerialiser : IBinarySerialiser<IMethodInfo>, IVersioned
{
}