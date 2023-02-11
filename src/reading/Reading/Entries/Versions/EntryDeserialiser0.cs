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

   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
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
      byte rawSeverityAndPurpose = reader.ReadByte();
      long rawTimestamp = reader.ReadInt64();
      ulong fileId = reader.ReadUInt64();
      uint line = reader.ReadUInt32();

      ushort rawKinds = reader.ReadUInt16();

      SeverityAndPurpose severityAndPurpose = (SeverityAndPurpose)rawSeverityAndPurpose;
      TimeSpan timestamp = new TimeSpan(rawTimestamp);
      Dictionary<ComponentKind, IComponent> components = new Dictionary<ComponentKind, IComponent>();

      ComponentKind kinds = (ComponentKind)rawKinds;
      foreach (ComponentKind kind in kinds.SplitValuesAscending())
      {
         IComponent component = _componentDeserialiser.Deserialise(reader, kind);
         components.Add(kind, component);
      }

      return EntryFactory.Version0(id, severityAndPurpose, timestamp, fileId, line, components);
   }
   #endregion
}
