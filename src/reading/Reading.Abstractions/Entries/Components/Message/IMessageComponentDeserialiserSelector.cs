using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Entries.Components.Message;

/// <inheritdoc/>
public interface IMessageComponentDeserialiserSelector : IDeserialiserSelector<IMessageComponentDeserialiser>
{
}
