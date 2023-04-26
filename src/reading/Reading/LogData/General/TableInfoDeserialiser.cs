using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.General;

/// <summary>
/// A deserialiser for <see cref="ITableInfo"/>.
/// </summary>
public sealed class TableInfoDeserialiser : IDeserialiser<ITableInfo>
{
   #region Fields
   private readonly IPrimitiveDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TableInfoDeserialiser"/>.</summary>
   /// <param name="deserialiser">The <see cref="IPrimitiveDeserialiser"/> to use.</param>
   public TableInfoDeserialiser(IPrimitiveDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ITableInfo Deserialise(BinaryReader reader)
   {
      int tableSize = reader.Read7BitEncodedInt();
      Dictionary<uint, object?> table = new Dictionary<uint, object?>(tableSize);
      for (int i = 0; i < tableSize; i++)
      {
         uint keyId = reader.ReadUInt32();
         object? value = _deserialiser.Deserialise(reader);

         table.Add(keyId, value);
      }

      ITableInfo tableInfo = new TableInfo(table);

      return tableInfo;
   }
   #endregion
}