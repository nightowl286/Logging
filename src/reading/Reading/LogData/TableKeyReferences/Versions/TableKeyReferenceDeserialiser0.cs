using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Reading.Abstractions.LogData.TableKeyReferences;

namespace TNO.Logging.Reading.LogData.TableKeyReferences.Versions;

/// <summary>
/// A deserialiser for <see cref="TableKeyReference"/>, version #0.
/// </summary>
public sealed class TableKeyReferenceDeserialiser0 : ITableKeyReferenceDeserialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public TableKeyReference Deserialise(BinaryReader reader)
   {
      string TableKey = reader.ReadString();
      uint id = reader.ReadUInt32();

      return TableKeyReferenceFactory.Version0(TableKey, id);
   }
   #endregion
}