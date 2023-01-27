using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Abstractions.Entries.Components;

/// <inheritdoc/>
public interface IMessageComponentSerialiser : IBinarySerialiser<IMessageComponent>, IVersioned
{
}
