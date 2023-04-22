using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers;

/// <summary>
/// A serialiser for <see cref="DataVersionMap"/>.
/// </summary>
public class DataVersionMapSerialiser : ISerialiser<DataVersionMap>, ISerialiser<DataKindVersion>
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, DataVersionMap data)
   {
      int count = data.Count;
      writer.Write(count);

      foreach (DataKindVersion dataKindVersion in data)
         Serialise(writer, dataKindVersion);
   }

   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, DataKindVersion data)
   {
      VersionedDataKind dataKind = data.DataKind;

      ushort rawKind = (ushort)dataKind;
      uint version = data.Version;

      writer.Write(rawKind);
      writer.Write(version);
   }

   /// <inheritdoc/>
   public ulong Count(DataVersionMap data)
   {
      ulong total = sizeof(int);
      foreach (DataKindVersion dataKindVersion in data)
         total += Count(dataKindVersion);

      return total;
   }

   /// <inheritdoc/>
   public ulong Count(DataKindVersion data) => sizeof(ushort) + sizeof(uint);
   #endregion
}