using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Tables;

namespace TNO.Logging.Writing.Serialisers.LogData.Tables;

/// <summary>
/// A serialiser for <see cref="ITableInfo"/>.
/// </summary>
[Version(0)]
public sealed class TableInfoSerialiser : ITableInfoSerialiser
{
   #region Fields
   internal static readonly Dictionary<Type, TableDataKind> DataKinds = new Dictionary<Type, TableDataKind>()
   {
      { typeof(byte), TableDataKind.Byte },
      { typeof(sbyte), TableDataKind.SByte },
      { typeof(ushort), TableDataKind.UShort},
      { typeof(short), TableDataKind.Short },
      { typeof(uint), TableDataKind.UInt },
      { typeof(int), TableDataKind.Int },
      { typeof(ulong), TableDataKind.ULong },
      { typeof(long), TableDataKind.Long },

      { typeof(float), TableDataKind.Float },
      { typeof(double), TableDataKind.Double },
      { typeof(decimal), TableDataKind.Decimal },

      { typeof(char), TableDataKind.Char },
      { typeof(string), TableDataKind.String },
      { typeof(bool), TableDataKind.Bool },

      { typeof(TimeSpan), TableDataKind.TimeSpan },
      { typeof(DateTime), TableDataKind.DateTime },
      { typeof(DateTimeOffset), TableDataKind.DateTimeOffset },
      { typeof(TimeZoneInfo), TableDataKind.TimeZoneInfo },
   };
   private static readonly Dictionary<TableDataKind, ulong> DataKindSizes = new Dictionary<TableDataKind, ulong>()
   {
      { TableDataKind.Byte, sizeof(byte) },
      { TableDataKind.SByte, sizeof(sbyte) },
      { TableDataKind.UShort, sizeof(ushort) },
      { TableDataKind.Short, sizeof(short) },
      { TableDataKind.UInt, sizeof(uint) },
      { TableDataKind.Int, sizeof(int) },
      { TableDataKind.ULong, sizeof(ulong) },
      { TableDataKind.Long, sizeof(long) },

      { TableDataKind.Float, sizeof(float) },
      { TableDataKind.Double, sizeof(double) },
      { TableDataKind.Decimal, sizeof(decimal) },

      { TableDataKind.Bool, sizeof(bool) },

      { TableDataKind.TimeSpan, sizeof(long) },
      { TableDataKind.DateTime, sizeof(long) },
      { TableDataKind.DateTimeOffset, sizeof(long) * 2 },
   };
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
         SerialiseValue(writer, pair.Value);
      }
   }
   private void Serialise(BinaryWriter writer, ICollectionInfo data)
   {
      IReadOnlyCollection<object?> collection = data.Collection;
      int count = collection.Count;
      writer.Write7BitEncodedInt(count);

      foreach (object? value in collection)
         SerialiseValue(writer, value);
   }
   private static void Serialise(BinaryWriter writer, IUnknownTableValue unknownTableValue)
   {
      writer.Write(unknownTableValue.TypeId);
   }
   private void SerialiseValue(BinaryWriter writer, object? value)
   {
      if (value is null)
      {
         writer.Write((byte)TableDataKind.Null);
      }
      else if (value is ITableInfo nestedTable)
      {
         writer.Write((byte)TableDataKind.Table);
         Serialise(writer, nestedTable);
      }
      else if (value is ICollectionInfo nestedCollection)
      {
         writer.Write((byte)TableDataKind.Collection);
         Serialise(writer, nestedCollection);
      }
      else if (value is IUnknownTableValue unknownTableValue)
      {
         writer.Write((byte)TableDataKind.Unknown);
         Serialise(writer, unknownTableValue);
      }
      else
         SerialisePrimitive(writer, value);
   }
   private static void SerialisePrimitive(BinaryWriter writer, object value)
   {
      Type valueType = value.GetType();
      TableDataKind dataKind = DataKinds[valueType];

      writer.Write((byte)dataKind);
      #region Numeric
      if (value is byte @byte) writer.Write(@byte);
      else if (value is sbyte @sbyte) writer.Write(@sbyte);
      else if (value is ushort @ushort) writer.Write(@ushort);
      else if (value is short @short) writer.Write(@short);
      else if (value is uint @uint) writer.Write(@uint);
      else if (value is int @int) writer.Write(@int);
      else if (value is ulong @ulong) writer.Write(@ulong);
      else if (value is long @long) writer.Write(@long);
      else if (value is float @float) writer.Write(@float);
      else if (value is double @double) writer.Write(@double);
      else if (value is decimal @decimal) writer.Write(@decimal);
      #endregion
      else if (value is char @char) writer.Write(@char);
      else if (value is string @string) writer.Write(@string);
      else if (value is bool @bool) writer.Write(@bool);
      else if (value is TimeSpan timeSpan) writer.Write(timeSpan.Ticks);
      else if (value is DateTime dateTime) writer.Write(dateTime.Ticks);
      else if (value is DateTimeOffset dateTimeOffset)
      {
         writer.Write(dateTimeOffset.Ticks);
         writer.Write(dateTimeOffset.Offset.Ticks);
      }
      else if (value is TimeZoneInfo timeZoneInfo) writer.Write(timeZoneInfo.Id);
      else throw new InvalidOperationException($"Unknown table data type ({valueType}).");
   }

   /// <inheritdoc/>
   public ulong Count(ITableInfo data)
   {
      int tableSize = data.Table.Count;
      int tableSizeSize = BinaryWriterSizeHelper.Encoded7BitIntSize(tableSize);
      int keysSize = tableSize * sizeof(uint);

      ulong total = (ulong)(tableSizeSize + keysSize);
      foreach (object? value in data.Table.Values)
         total += CountValue(value);

      return total;
   }
   private ulong Count(ICollectionInfo data)
   {
      IReadOnlyCollection<object?> collection = data.Collection;
      int countSize = BinaryWriterSizeHelper.Encoded7BitIntSize(collection.Count);

      ulong total = 0;
      foreach (object? value in collection)
         total += CountValue(value);

      return total + (ulong)countSize;
   }
   private ulong CountValue(object? value)
   {
      ulong kindSize = sizeof(byte);
      ulong valueSize;

      if (value is null)
         valueSize = 0;
      else if (value is ITableInfo tableInfo)
         valueSize = Count(tableInfo);
      else if (value is ICollectionInfo collectionInfo)
         valueSize = Count(collectionInfo);
      else
         valueSize = CountPrimitive(value);

      return kindSize + valueSize;
   }
   private static ulong CountPrimitive(object value)
   {
      if (value is string @string)
         return (ulong)BinaryWriterSizeHelper.StringSize(@string);
      else if (value is char @char)
         return (ulong)BinaryWriterSizeHelper.CharSize(@char);
      else if (value is TimeZoneInfo @timeZoneInfo)
         return (ulong)BinaryWriterSizeHelper.StringSize(timeZoneInfo.Id);
      else
      {
         Type valueType = value.GetType();
         if (DataKinds.TryGetValue(valueType, out TableDataKind kind))
            return DataKindSizes[kind];

         return sizeof(ulong); // Unknown table value type id
      }
   }
   #endregion
}