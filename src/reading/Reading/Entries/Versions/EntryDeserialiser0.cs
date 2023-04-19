using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Entries.Components;

namespace TNO.Logging.Reading.Entries.Versions;

/// <summary>
/// A deserialiser for <see cref="IEntry"/>, version #0.
/// </summary>
[Version(0)]
public sealed class EntryDeserialiser0 : IDeserialiser<IEntry>
{
   #region Fields
   private readonly ComponentDeserialiserDispatcher _componentDeserialiserDispatcher;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="EntryDeserialiser0"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>
   public EntryDeserialiser0(IDeserialiser deserialiser)
   {
      _componentDeserialiserDispatcher = new ComponentDeserialiserDispatcher(deserialiser);
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IEntry Deserialise(BinaryReader reader)
   {
      ulong id = reader.ReadUInt64();
      ulong contextId = reader.ReadUInt64();
      ulong scope = reader.ReadUInt64();
      byte rawImportance = reader.ReadByte();
      long rawTimestamp = reader.ReadInt64();
      ulong fileId = reader.ReadUInt64();
      uint line = reader.ReadUInt32();

      ushort rawKinds = reader.ReadUInt16();

      ImportanceCombination Importance = (ImportanceCombination)rawImportance;
      TimeSpan timestamp = new TimeSpan(rawTimestamp);
      Dictionary<ComponentKind, IComponent> components = new Dictionary<ComponentKind, IComponent>();

      ComponentKind kinds = (ComponentKind)rawKinds;
      foreach (ComponentKind kind in kinds.SplitValuesAscending())
      {
         IComponent component = _componentDeserialiserDispatcher.Deserialise(reader, kind);
         components.Add(kind, component);
      }

      return EntryFactory.Version0(
         id,
         contextId,
         scope,
         Importance,
         timestamp,
         fileId,
         line,
         components);
   }
   #endregion
}