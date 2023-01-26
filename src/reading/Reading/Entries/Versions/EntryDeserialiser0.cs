using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries;
using TNO.Logging.Reading.Abstractions.Entries.Components;

namespace TNO.Logging.Reading.Entries.Versions;

/// <summary>
/// A deserialiser for <see cref="IEntry"/>, version #0.
/// </summary>
public sealed class EntryDeserialiser0 : IEntryDeserialiser
{
   #region Fields
   private IComponentDeserialiserDispatcher _componentDeserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="EntryDeserialiser0"/>.</summary>
   /// <param name="componentDeserialiser">The component deserialiser to use.</param>
   public EntryDeserialiser0(IComponentDeserialiserDispatcher componentDeserialiser)
   {
      _componentDeserialiser = componentDeserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IEntry Deserialise(BinaryReader reader)
   {
      ulong id = reader.ReadUInt64();
      ushort rawKinds = reader.ReadUInt16();

      Dictionary<ComponentKind, IComponent> components = new Dictionary<ComponentKind, IComponent>();

      ComponentKind kinds = (ComponentKind)rawKinds;
      foreach (ComponentKind kind in kinds.SplitValuesAscending())
      {
         IComponent component = _componentDeserialiser.Deserialise(reader, kind);
         components.Add(kind, component);
      }

      return EntryFactory.Version0(id, components);
   }
   #endregion
}
