using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.MethodInfos;
using TNO.Logging.Reading.Abstractions.LogData.Methods.MethodInfos;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.MethodInfos.Versions;

namespace TNO.Logging.Reading.LogData.MethodInfos;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IMethodInfo"/>.
/// </summary>
internal class MethodInfoDeserialiserSelector : DeserialiserSelectorBase<IMethodInfoDeserialiser>, IMethodInfoDeserialiserSelector
{
   public MethodInfoDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<MethodInfoDeserialiser0>(0);
   }
}