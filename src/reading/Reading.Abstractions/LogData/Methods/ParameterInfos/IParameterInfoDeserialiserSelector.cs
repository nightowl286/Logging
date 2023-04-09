using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.ParameterInfos;

/// <inheritdoc/>
public interface IParameterInfoDeserialiserSelector : IDeserialiserSelector<IParameterInfoDeserialiser>
{
}