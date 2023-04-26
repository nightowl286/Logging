using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData;

/// <summary>
/// A serialiser for <see cref="TableKeyReference"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.TableKeyReference)]
public class TableKeyReferenceSerialiser : ISerialiser<TableKeyReference>
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, TableKeyReference data)
   {
      string key = data.Key;
      uint id = data.Id;

      writer.Write(key);
      writer.Write(id);
   }

   /// <inheritdoc/>
   public int Count(TableKeyReference data)
   {
      string key = data.Key;

      int keySize = BinaryWriterSizeHelper.StringSize(key);

      return keySize + sizeof(uint);
   }
   #endregion
}