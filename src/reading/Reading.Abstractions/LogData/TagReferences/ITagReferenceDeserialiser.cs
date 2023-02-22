using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.TagReferences;

/// <inheritdoc/>
public interface ITagReferenceDeserialiser : IBinaryDeserialiser<TagReference>, IVersioned
{
}