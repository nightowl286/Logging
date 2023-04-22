using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading;

/// <summary>
/// A deserialiser for <see cref="DataVersionMap"/>.
/// </summary>
public class DataVersionMapDeserialiser : IDeserialiser<DataVersionMap>, IDeserialiser<DataKindVersion>
{
   #region Methods
   /// <inheritdoc/>
   DataVersionMap IDeserialiser<DataVersionMap>.Deserialise(BinaryReader reader)
   {
      DataVersionMap map = new DataVersionMap();

      IDeserialiser<DataKindVersion> deserialiser = this;

      int count = reader.ReadInt32();
      for (int i = 0; i < count; i++)
      {
         DataKindVersion dataKindVersion = deserialiser.Deserialise(reader);

         map.Add(dataKindVersion);
      }

      return map;
   }

   DataKindVersion IDeserialiser<DataKindVersion>.Deserialise(BinaryReader reader)
   {
      ushort rawKind = reader.ReadUInt16();
      uint version = reader.ReadUInt32();

      VersionedDataKind dataKind = (VersionedDataKind)rawKind;

      return new DataKindVersion(dataKind, version);
   }
   #endregion
}