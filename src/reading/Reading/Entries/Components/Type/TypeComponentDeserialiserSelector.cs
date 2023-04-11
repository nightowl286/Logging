using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.Components.Type;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.Type.Versions;

namespace TNO.Logging.Reading.Entries.Components.Type;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="ITypeComponentDeserialiser"/>.
/// </summary>
internal class TypeComponentDeserialiserSelector : DeserialiserSelectorBase<ITypeComponentDeserialiser>, ITypeComponentDeserialiserSelector
{
   public TypeComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<TypeComponentDeserialiser0>(0);
   }
}