using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Entries;

/// <summary>
/// A serialiser for <see cref="IEntry"/>.
/// </summary>
[Version(0)]
public class EntrySerialiser : IEntrySerialiser
{
   #region Fields
   private readonly IComponentSerialiserDispatcher _componentSerialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="EntrySerialiser"/>.</summary>
   /// <param name="componentSerialiser">The component serialiser to use.</param>
   public EntrySerialiser(IComponentSerialiserDispatcher componentSerialiser)
   {
      _componentSerialiser = componentSerialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IEntry data)
   {
      ulong id = data.Id;
      ulong contextId = data.ContextId;
      ulong scope = data.Scope;
      byte rawImportance = (byte)data.Importance;
      long rawTimestamp = data.Timestamp.Ticks;
      ulong fileId = data.FileId;
      uint line = data.LineInFile;

      ComponentKind kinds = data.Components.Keys.CombineFlags();
      ushort rawKinds = (ushort)kinds;

      writer.Write(id);
      writer.Write(contextId);
      writer.Write(scope);
      writer.Write(rawImportance);
      writer.Write(rawTimestamp);
      writer.Write(fileId);
      writer.Write(line);
      writer.Write(rawKinds);
      foreach (ComponentKind possibleKind in EnumExtensions.GetValuesAscending<ComponentKind>())
      {
         if (data.Components.TryGetValue(possibleKind, out IComponent? component))
            _componentSerialiser.Serialise(writer, component);
      }
   }

   /// <inheritdoc/>
   public ulong Count(IEntry data)
   {
      int headerSize =
         (sizeof(ulong) * 4) +
         sizeof(long) +
         sizeof(uint) +
         sizeof(ushort) +
         sizeof(byte);

      ulong componentSizes = 0;
      foreach (IComponent component in data.Components.Values)
         componentSizes += _componentSerialiser.Count(component);

      return componentSizes + (ulong)headerSize;
   }
   #endregion
}