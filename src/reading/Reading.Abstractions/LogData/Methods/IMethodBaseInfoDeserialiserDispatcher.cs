using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.Methods;

/// <summary>
/// Denotes a <see cref="IBinaryDeserialiser{T}"/> dispatcher that will
/// deserialise a <see cref="IMethodBaseInfo"/> based on its <see cref="MethodKind"/>.
/// </summary>
public interface IMethodBaseInfoDeserialiserDispatcher : IBinaryDeserialiser<IMethodBaseInfo>
{
}
