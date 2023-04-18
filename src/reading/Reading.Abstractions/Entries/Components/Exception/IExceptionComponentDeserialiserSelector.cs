using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Entries.Components.Exception;

/// <inheritdoc/>
public interface IExceptionComponentDeserialiserSelector : IDeserialiserSelector<IExceptionComponentDeserialiser>
{
}