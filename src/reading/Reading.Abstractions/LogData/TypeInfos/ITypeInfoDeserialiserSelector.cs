using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.TypeInfos;

/// <inheritdoc/>
public interface ITypeInfoDeserialiserSelector : IDeserialiserSelector<ITypeInfoDeserialiser>
{
}