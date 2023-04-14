using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.Tables;

/// <inheritdoc/>
public interface ITableInfoDeserialiserSelector : IDeserialiserSelector<ITableInfoDeserialiser>
{
}