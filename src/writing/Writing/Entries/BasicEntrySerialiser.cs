using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions.Entries;

namespace TNO.Logging.Writing.Entries;

internal sealed class BasicEntrySerialiser : IBasicEntrySerialiser
{
   #region Methods
   public void Serialise(BinaryWriter writer, IBasicEntry data)
   {
      ulong id = data.Id;
      ComponentKind kinds = data.ComponentKinds;

      ushort rawKinds = (ushort)kinds;

      writer.Write(id);
      writer.Write(rawKinds);
   }
   #endregion
}
