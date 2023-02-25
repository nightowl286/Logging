using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.Components.EntryLink;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.EntryLink.Versions;

namespace TNO.Logging.Reading.Entries.Components.EntryLink;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IEntryLinkComponentDeserialiser"/>.
/// </summary>
internal class EntryLinkComponentDeserialiserSelector : DeserialiserSelectorBase<IEntryLinkComponentDeserialiser>, IEntryLinkComponentDeserialiserSelector
{
   public EntryLinkComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<EntryLinkComponentDeserialiser0>(0);
   }
}