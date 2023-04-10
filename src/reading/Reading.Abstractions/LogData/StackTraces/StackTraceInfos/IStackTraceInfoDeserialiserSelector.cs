using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackTraceInfos;

/// <inheritdoc/>
public interface IStackTraceInfoDeserialiserSelector : IDeserialiserSelector<IStackTraceInfoDeserialiser>
{
}