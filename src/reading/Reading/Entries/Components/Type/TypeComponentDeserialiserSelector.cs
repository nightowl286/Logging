using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.Type.Versions;

namespace TNO.Logging.Reading.Entries.Components.Type;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="ITypeComponent"/>.
/// </summary>
internal class TypeComponentDeserialiserSelector : DeserialiserSelectorBase<ITypeComponent>
{
   public TypeComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<TypeComponentDeserialiser0>(0);
   }
}