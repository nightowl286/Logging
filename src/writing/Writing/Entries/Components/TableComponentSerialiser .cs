using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="ITableComponent"/>.
/// </summary>
public sealed class TableComponentSerialiser : ITableComponentSerialiser
{
   #region Fields
   private static readonly Dictionary<Type, TableDataKind> DataKinds = new Dictionary<Type, TableDataKind>()
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

   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ITableComponent data)
   {
      int tableSize = data.Table.Count;
      writer.Write7BitEncodedInt(tableSize);

      foreach (KeyValuePair<uint, object> pair in data.Table)
      {
         writer.Write(pair.Key);
         Type valueType = pair.Value.GetType();

         TableDataKind dataKind = DataKinds[valueType];
         writer.Write((byte)dataKind);

         object value = pair.Value;
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
            writer.Write(dateTimeOffset.UtcTicks);
            writer.Write(dateTimeOffset.Offset.Ticks);
         }
         else if (value is TimeZoneInfo timeZoneInfo) writer.Write(timeZoneInfo.Id);
         else throw new InvalidOperationException($"Unknown table data type ({valueType}).");
      }
   }

   /// <inheritdoc/>
   public ulong Count(ITableComponent data)
   {
      int tableSize = data.Table.Count;
      int tableSizeSize = BinaryWriterSizeHelper.Encoded7BitIntSize(tableSize);
      int keysSize = tableSize * sizeof(uint);
      int kindSize = tableSize * sizeof(byte);

      ulong total = (ulong)(tableSizeSize + keysSize + kindSize);
      foreach (object value in data.Table.Values)
      {
         if (value is string @string)
            total += (ulong)BinaryWriterSizeHelper.StringSize(@string);
         else if (value is char @char)
            total += (ulong)BinaryWriterSizeHelper.CharSize(@char);
         else if (value is TimeZoneInfo @timeZoneInfo)
            total += (ulong)BinaryWriterSizeHelper.StringSize(timeZoneInfo.Id);
         else
         {
            TableDataKind kind = DataKinds[value.GetType()];
            total += DataKindSizes[kind];
         }
      }

      return total;
   }
   #endregion
}