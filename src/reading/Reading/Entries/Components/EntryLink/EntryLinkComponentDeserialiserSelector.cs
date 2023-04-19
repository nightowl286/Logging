using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.EntryLink.Versions;

namespace TNO.Logging.Reading.Entries.Components.EntryLink;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IEntryLinkComponent"/>.
/// </summary>
internal class EntryLinkComponentDeserialiserSelector : DeserialiserSelectorBase<IEntryLinkComponent>
{
   public EntryLinkComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<EntryLinkComponentDeserialiser0>(0);
   }
}