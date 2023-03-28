using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.TypeInfos;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.TypeInfos.Versions;

namespace TNO.Logging.Reading.LogData.TypeInfos;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="ITypeInfo"/>.
/// </summary>
internal class TypeInfoDeserialiserSelector : DeserialiserSelectorBase<ITypeInfoDeserialiser>, ITypeInfoDeserialiserSelector
{
   public TypeInfoDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<TypeInfoDeserialiser0>(0);
   }
}