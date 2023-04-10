using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackFrameInfos;

/// <inheritdoc/>
public interface IStackFrameInfoDeserialiserSelector : IDeserialiserSelector<IStackFrameInfoDeserialiser>
{
}