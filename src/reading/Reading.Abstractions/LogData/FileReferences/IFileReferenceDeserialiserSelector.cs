using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.FileReferences;

/// <inheritdoc/>
public interface IFileReferenceDeserialiserSelector : IDeserialiserSelector<IFileReferenceDeserialiser>
{
}