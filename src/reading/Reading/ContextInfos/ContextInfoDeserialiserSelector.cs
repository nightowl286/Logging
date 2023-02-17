using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Reading.Abstractions.ContextInfos;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.ContextInfos.Versions;
using TNO.Logging.Reading.Deserialisers;

namespace TNO.Logging.Reading.ContextInfos;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="ContextInfo"/>.
/// </summary>
internal class ContextInfoDeserialiserSelector : DeserialiserSelectorBase<IContextInfoDeserialiser>, IContextInfoDeserialiserSelector
{
   public ContextInfoDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<ContextInfoDeserialiser0>(0);
   }
}