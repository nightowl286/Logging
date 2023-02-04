using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Entries;

/// <inheritdoc/>
public interface IEntryDeserialiserSelector : IDeserialiserSelector<IEntryDeserialiser>
{
}
