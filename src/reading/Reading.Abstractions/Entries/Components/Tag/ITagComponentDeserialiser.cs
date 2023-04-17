using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Entries.Components.Tag;


/// <inheritdoc/>
public interface ITagComponentDeserialiser : IBinaryDeserialiser<ITagComponent>, IVersioned
{
}