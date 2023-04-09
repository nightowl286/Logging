using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.ConstructorInfos;

namespace TNO.Logging.Reading.Abstractions.LogData.ParameterInfos;

/// <inheritdoc/>
public interface IConstructorInfoDeserialiserSelector : IDeserialiserSelector<IConstructorInfoDeserialiser>
{
}