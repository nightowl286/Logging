using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.AssemblyInfos;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.AssemblyInfos.Versions;

namespace TNO.Logging.Reading.LogData.AssemblyInfos;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IAssemblyInfo"/>.
/// </summary>
internal class AssemblyInfoDeserialiserSelector : DeserialiserSelectorBase<IAssemblyInfoDeserialiser>, IAssemblyInfoDeserialiserSelector
{
   public AssemblyInfoDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<AssemblyInfoDeserialiser0>(0);
   }
}