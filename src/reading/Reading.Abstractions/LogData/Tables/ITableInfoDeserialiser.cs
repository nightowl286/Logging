using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.Tables;

/// <inheritdoc/>
public interface ITableInfoDeserialiser : IBinaryDeserialiser<ITableInfo>, IVersioned
{
}