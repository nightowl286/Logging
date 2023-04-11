using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Entries.Components.StackTrace;

/// <inheritdoc/>
public interface IStackTraceComponentDeserialiserSelector : IDeserialiserSelector<IStackTraceComponentDeserialiser>
{
}
