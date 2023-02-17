using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.ContextInfos;

/// <inheritdoc/>
public interface IContextInfoDeserialiserSelector : IDeserialiserSelector<IContextInfoDeserialiser>
{
}