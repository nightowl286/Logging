using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers;

/// <summary>
/// A serialiser for <see cref="DataVersionMap"/>.
/// </summary>
public class DataVersionMapSerialiser : ISerialiser<DataVersionMap>
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, DataVersionMap data)
   {
      int count = data.Count;
      writer.Write(count);

      foreach (KeyValuePair<VersionedDataKind, uint> pair in data)
      {
         ushort rawKind = (ushort)pair.Key;

         writer.Write(rawKind);
         writer.Write(pair.Value);
      }
   }

   /// <inheritdoc/>
   public ulong Count(DataVersionMap data)
   {
      int count = data.Count;
      int size =
         sizeof(int) +
         (sizeof(ushort) * count) +
         (sizeof(int) * count);

      return (ulong)size;
   }
   #endregion
}