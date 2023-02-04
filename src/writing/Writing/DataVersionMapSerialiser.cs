using TNO.Logging.Common.Abstractions;
using TNO.Logging.Writing.Abstractions;

namespace TNO.Logging.Writing;

/// <summary>
/// A serialiser for <see cref="DataVersionMap"/>.
/// </summary>
public class DataVersionMapSerialiser : IDataVersionMapSerialiser
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
   #endregion
}
