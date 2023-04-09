using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.Methods.MethodInfos;

/// <inheritdoc/>
public interface IMethodInfoDeserialiser : IBinaryDeserialiser<IMethodInfo>, IVersioned
{
}
