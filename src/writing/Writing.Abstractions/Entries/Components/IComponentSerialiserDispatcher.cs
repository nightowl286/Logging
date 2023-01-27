using System.IO;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ISerialiser{T}"/> dispatcher that
/// will serialise a given <see cref="IComponent"/> based
/// on its <see cref="IComponent.Kind"/>.
/// </summary>
public interface IComponentSerialiserDispatcher
{
   #region Methods
   /// <summary>Serialises the given <paramref name="data"/> using the given <paramref name="writer"/>.</summary>
   /// <param name="writer">The writer to use.</param>
   /// <param name="data">The data to serialise.</param>
   void Serialise(BinaryWriter writer, IComponent data);
   #endregion
}
