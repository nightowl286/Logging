using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackTraceInfos;

/// <inheritdoc/>
public interface IStackTraceInfoDeserialiser : IBinaryDeserialiser<IStackTraceInfo>, IVersioned
{
}
