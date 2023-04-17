using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.TagReferences;

/// <inheritdoc/>
public interface ITagReferenceDeserialiser : IBinaryDeserialiser<TagReference>, IVersioned
{
}