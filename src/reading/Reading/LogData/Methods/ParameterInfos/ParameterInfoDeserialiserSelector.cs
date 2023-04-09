using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.Methods.ParameterInfos;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.Methods.ParameterInfos.Versions;

namespace TNO.Logging.Reading.LogData.Methods.ParameterInfos;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IParameterInfo"/>.
/// </summary>
internal class ParameterInfoDeserialiserSelector : DeserialiserSelectorBase<IParameterInfoDeserialiser>, IParameterInfoDeserialiserSelector
{
   public ParameterInfoDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<ParameterInfoDeserialiser0>(0);
   }
}