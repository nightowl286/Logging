using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Entries.Components.EntryLink;


/// <inheritdoc/>
public interface IEntryLinkComponentDeserialiser : IBinaryDeserialiser<IEntryLinkComponent>, IVersioned
{
}