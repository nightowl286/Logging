using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Entries;

/// <summary>
/// A serialiser for <see cref="IEntry"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.Entry)]
public class EntrySerialiser : ISerialiser<IEntry>
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="EntrySerialiser"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public EntrySerialiser(ISerialiser serialiser)
   {
      _serialiser = serialiser;
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
            _serialiser.Serialise(writer, component);
      }
   }

   /// <inheritdoc/>
   public int Count(IEntry data)
   {
      int headerSize =
         (sizeof(ulong) * 4) +
         sizeof(long) +
         sizeof(uint) +
         sizeof(ushort) +
         sizeof(byte);

      int componentSizes = 0;
      foreach (IComponent component in data.Components.Values)
         componentSizes += _serialiser.Count(component);

      return componentSizes + headerSize;
   }
   #endregion
}