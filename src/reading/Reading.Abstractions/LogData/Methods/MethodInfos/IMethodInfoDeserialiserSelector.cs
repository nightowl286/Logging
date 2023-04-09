using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.MethodInfos;

namespace TNO.Logging.Reading.Abstractions.LogData.ParameterInfos;

/// <inheritdoc/>
public interface IMethodInfoDeserialiserSelector : IDeserialiserSelector<IMethodInfoDeserialiser>
{
}