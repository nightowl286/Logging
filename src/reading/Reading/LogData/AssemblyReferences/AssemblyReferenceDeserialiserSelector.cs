using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.AssemblyReferences;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.AssemblyReferences.Versions;

namespace TNO.Logging.Reading.LogData.AssemblyReferences;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="AssemblyReference"/>.
/// </summary>
internal class AssemblyReferenceDeserialiserSelector : DeserialiserSelectorBase<IAssemblyReferenceDeserialiser>, IAssemblyReferenceDeserialiserSelector
{
   public AssemblyReferenceDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<AssemblyReferenceDeserialiser0>(0);
   }
}