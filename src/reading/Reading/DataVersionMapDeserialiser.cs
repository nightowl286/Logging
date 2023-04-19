using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading;

/// <summary>
/// A deserialiser for <see cref="DataVersionMap"/>.
/// </summary>
public class DataVersionMapDeserialiser : IDeserialiser<DataVersionMap>
{
   #region Methods
   /// <inheritdoc/>
   public DataVersionMap Deserialise(BinaryReader reader)
   {
      DataVersionMap map = new DataVersionMap();

      int count = reader.ReadInt32();
      for (int i = 0; i < count; i++)
      {
         ushort rawKind = reader.ReadUInt16();
         VersionedDataKind kind = (VersionedDataKind)rawKind;

         uint version = reader.ReadUInt32();

         map.Add(kind, version);
      }

      return map;
   }
   #endregion
}