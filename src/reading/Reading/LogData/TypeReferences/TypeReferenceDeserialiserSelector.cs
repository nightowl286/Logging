using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.TypeReferences;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.TypeReferences.Versions;

namespace TNO.Logging.Reading.LogData.TypeReferences;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="TypeReference"/>.
/// </summary>
internal class TypeReferenceDeserialiserSelector : DeserialiserSelectorBase<ITypeReferenceDeserialiser>, ITypeReferenceDeserialiserSelector
{
   public TypeReferenceDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<TypeReferenceDeserialiser0>(0);
   }
}