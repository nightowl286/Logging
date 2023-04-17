using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers.LogData.StackTraces;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.StackTraceInfo)]
public interface IStackTraceInfoSerialiser : IBinarySerialiser<IStackTraceInfo>, IVersioned
{
}