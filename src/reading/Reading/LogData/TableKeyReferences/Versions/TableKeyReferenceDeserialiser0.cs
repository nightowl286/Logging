using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.TableKeyReferences.Versions;

/// <summary>
/// A deserialiser for <see cref="TableKeyReference"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.TableKeyReference)]
public sealed class TableKeyReferenceDeserialiser0 : IDeserialiser<TableKeyReference>
{
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