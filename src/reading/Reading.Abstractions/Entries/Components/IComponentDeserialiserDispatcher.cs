using System.IO;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="IDeserialiser{T}"/> dispatcher that 
/// will deserialise a <see cref="IComponent"/> based on a 
/// given <see cref="ComponentKind"/>.
/// </summary>
public interface IComponentDeserialiserDispatcher
{
   #region Methods
   /// <summary>Deserialises an <see cref="IComponent"/> based on the given <paramref name="componentKind"/>.</summary>
   /// <param name="reader">The reader to use.</param>
   /// <param name="componentKind">The component kind to deserialise.</param>
   /// <returns>The deserialised <see cref="IComponent"/> instance.</returns>
   IComponent Deserialise(BinaryReader reader, ComponentKind componentKind);
   #endregion
}