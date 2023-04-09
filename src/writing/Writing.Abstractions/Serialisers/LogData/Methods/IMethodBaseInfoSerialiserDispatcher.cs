using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ISerialiser{T}"/> dispatcher that will serialise
/// a given <see cref="IMethodBaseInfo"/> based on its <see cref="MethodKind"/>.
/// </summary>
public interface IMethodBaseInfoSerialiserDispatcher : IBinarySerialiser<IMethodBaseInfo>
{
}