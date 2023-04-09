using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers.LogData.Parameters;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.ParameterInfo)]
public interface IParameterInfoSerialiser : IBinarySerialiser<IParameterInfo>, IVersioned
{
}