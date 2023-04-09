using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.MethodInfos;

namespace TNO.Logging.Reading.Abstractions.LogData.Methods.MethodInfos;

/// <inheritdoc/>
public interface IMethodInfoDeserialiserSelector : IDeserialiserSelector<IMethodInfoDeserialiser>
{
}