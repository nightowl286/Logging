using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.ContextInfos;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.ContextInfos.Versions;

namespace TNO.Logging.Reading.LogData.ContextInfos;

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