using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.Assembly.Versions;

namespace TNO.Logging.Reading.Entries.Components.Assembly;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IAssemblyComponent"/>.
/// </summary>
internal class AssemblyComponentDeserialiserSelector : DeserialiserSelectorBase<IAssemblyComponent>
{
   public AssemblyComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<AssemblyComponentDeserialiser0>(0);
   }
}