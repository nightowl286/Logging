using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.TagReferences;

/// <inheritdoc/>
public interface ITagReferenceDeserialiserSelector : IDeserialiserSelector<ITagReferenceDeserialiser>
{
}