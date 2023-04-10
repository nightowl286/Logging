using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers.LogData.StackTraces;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.StackFrameInfo)]
public interface IStackFrameInfoSerialiser : IBinarySerialiser<IStackFrameInfo>, IVersioned
{
}