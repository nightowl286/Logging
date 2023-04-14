using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData;

namespace TNO.Logging.Writing.Serialisers.LogData;

/// <summary>
/// A serialiser for <see cref="TableKeyReference"/>.
/// </summary>
public class TableKeyReferenceSerialiser : ITableKeyReferenceSerialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;

   #endregion

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
   public ulong Count(TableKeyReference data)
   {
      string key = data.Key;

      int keySize = BinaryWriterSizeHelper.StringSize(key);

      return (ulong)(keySize + sizeof(uint));
   }
   #endregion
}