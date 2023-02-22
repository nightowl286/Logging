using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.TagReferences;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.TagReferences.Versions;

namespace TNO.Logging.Reading.LogData.TagReferences;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="TagReference"/>.
/// </summary>
internal class TagReferenceDeserialiserSelector : DeserialiserSelectorBase<ITagReferenceDeserialiser>, ITagReferenceDeserialiserSelector
{
   public TagReferenceDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<TagReferenceDeserialiser0>(0);
   }
}