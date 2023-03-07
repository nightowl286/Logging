using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.Components.Assembly;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.Assembly.Versions;

namespace TNO.Logging.Reading.Entries.Components.Assembly;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IAssemblyComponentDeserialiser"/>.
/// </summary>
internal class AssemblyComponentDeserialiserSelector : DeserialiserSelectorBase<IAssemblyComponentDeserialiser>, IAssemblyComponentDeserialiserSelector
{
   public AssemblyComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<AssemblyComponentDeserialiser0>(0);
   }
}