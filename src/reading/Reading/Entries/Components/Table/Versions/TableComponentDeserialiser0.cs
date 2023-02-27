using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Table;

namespace TNO.Logging.Reading.Entries.Components.Table.Versions;

/// <summary>
/// A deserialiser for <see cref="ITableComponent"/>, version #0.
/// </summary>
public sealed class TableComponentDeserialiser0 : ITableComponentDeserialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ITableComponent Deserialise(BinaryReader reader)
   {
      int tableSize = reader.Read7BitEncodedInt();
      Dictionary<uint, object> table = new Dictionary<uint, object>(tableSize);
      for (int i = 0; i < tableSize; i++)
      {
         uint keyId = reader.ReadUInt32();
         TableDataKind dataKind = (TableDataKind)reader.ReadByte();

         object value = dataKind switch
         {
            TableDataKind.Byte => reader.ReadByte(),
            TableDataKind.SByte => reader.ReadSByte(),
            TableDataKind.UShort => reader.ReadUInt16(),
            TableDataKind.Short => reader.ReadInt16(),
            TableDataKind.UInt => reader.ReadUInt32(),
            TableDataKind.Int => reader.ReadInt32(),
            TableDataKind.ULong => reader.ReadUInt64(),
            TableDataKind.Long => reader.ReadInt64(),

            TableDataKind.Float => reader.ReadSingle(),
            TableDataKind.Double => reader.ReadDouble(),
            TableDataKind.Decimal => reader.ReadDecimal(),

            TableDataKind.Char => reader.ReadChar(),
            TableDataKind.String => reader.ReadString(),
            TableDataKind.Bool => reader.ReadBoolean(),

            TableDataKind.TimeSpan => ReadTimeSpan(reader),
            TableDataKind.DateTime => ReadDateTime(reader),
            TableDataKind.DateTimeOffset => ReadDateTimeOffset(reader),
            TableDataKind.TimeZoneInfo => ReadTimeZoneInfo(reader),

            _ => throw new ArgumentException($"Unknown table data kind ({dataKind}).")
         };

         table.Add(keyId, value);
      }

      ITableComponent component = TableComponentFactory.Version0(table);

      return component;
   }
   #endregion

   #region Helpers
   private static TimeSpan ReadTimeSpan(BinaryReader reader)
   {
      long ticks = reader.ReadInt64();
      return new TimeSpan(ticks);
   }
   private static DateTime ReadDateTime(BinaryReader reader)
   {
      long ticks = reader.ReadInt64();
      return new DateTime(ticks);
   }
   private static DateTimeOffset ReadDateTimeOffset(BinaryReader reader)
   {
      long datetimeTicks = reader.ReadInt64();
      long offsetTicks = reader.ReadInt64();

      TimeSpan offset = new TimeSpan(offsetTicks);

      return new DateTimeOffset(datetimeTicks, offset);
   }
   private static TimeZoneInfo ReadTimeZoneInfo(BinaryReader reader)
   {
      string id = reader.ReadString();
      return TimeZoneInfo.FindSystemTimeZoneById(id);
   }
   #endregion
}