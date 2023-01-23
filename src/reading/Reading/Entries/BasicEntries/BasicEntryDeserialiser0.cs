using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Entries;
using TNO.Logging.Reading.Abstractions.Entries.BasicEntries;

namespace TNO.Logging.Reading.Entries.BasicEntries;
internal sealed class BasicEntryDeserialiser0 : IBasicEntryDeserialiser
{
   #region Methods
   public IBasicEntry Deserialise(BinaryReader reader)
   {
      ulong id = reader.ReadUInt64();
      ushort rawKinds = reader.ReadUInt16();

      ComponentKind kinds = (ComponentKind)rawKinds;

      BasicEntry basicEntry = new BasicEntry(id, kinds);

      return basicEntry;
   }
   #endregion
}
