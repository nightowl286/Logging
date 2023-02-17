using TNO.Logging.Common.Abstractions;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.ContextInfos;

/// <inheritdoc/>
public interface IContextInfoDeserialiser : IBinaryDeserialiser<ContextInfo>, IVersioned
{
}
