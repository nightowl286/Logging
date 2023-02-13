using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.FileReferences;

/// <inheritdoc/>
public interface IFileReferenceDeserialiserSelector : IDeserialiserSelector<IFileReferenceDeserialiser>
{
}