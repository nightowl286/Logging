using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData.General;

/// <summary>
/// A serialiser for <see cref="ITableInfo"/>.
/// </summary>
public sealed class TableInfoSerialiser : ISerialiser<ITableInfo>
{
   #region Fields
   private readonly IPrimitiveSerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TableInfoSerialiser"/>.</summary>
   /// <param name="serialiser">The <see cref="IPrimitiveSerialiser"/> to use.</param>
   public TableInfoSerialiser(IPrimitiveSerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ITableInfo data)
   {
      int tableSize = data.Table.Count;
      writer.Write7BitEncodedInt(tableSize);

      foreach (KeyValuePair<uint, object?> pair in data.Table)
      {
         writer.Write(pair.Key);
         _serialiser.Serialise(writer, pair.Value);
      }
   }

   /// <inheritdoc/>
   public int Count(ITableInfo data)
   {
      int tableSize = data.Table.Count;
      int tableSizeSize = BinaryWriterSizeHelper.Encoded7BitIntSize(tableSize);
      int keysSize = tableSize * sizeof(uint);

      int total = tableSizeSize + keysSize;
      foreach (object? value in data.Table.Values)
         total += _serialiser.Count(value);

      return total;
   }
   #endregion
}