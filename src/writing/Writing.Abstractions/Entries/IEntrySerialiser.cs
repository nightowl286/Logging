using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Abstractions.Entries;

/// <inheritdoc/>
public interface IEntrySerialiser : IBinarySerialiser<IEntry>, IVersioned
{
}
