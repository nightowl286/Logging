using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Entries;

/// <inheritdoc/>
public interface IEntryDeserialiser : IBinaryDeserialiser<IEntry>, IVersioned
{
}