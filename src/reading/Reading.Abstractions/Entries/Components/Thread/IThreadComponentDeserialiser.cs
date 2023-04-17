using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Entries.Components.Thread;

/// <inheritdoc/>
public interface IThreadComponentDeserialiser : IBinaryDeserialiser<IThreadComponent>, IVersioned
{
}
