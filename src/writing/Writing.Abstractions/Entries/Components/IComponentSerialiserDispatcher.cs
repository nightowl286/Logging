using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ISerialiser{T}"/> dispatcher that
/// will serialise a given <see cref="IComponent"/> based
/// on its <see cref="IComponent.Kind"/>.
/// </summary>
public interface IComponentSerialiserDispatcher : IBinarySerialiser<IComponent>
{
}