using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.Tables.Versions;

/// <summary>
/// A deserialiser for <see cref="ITableInfo"/>, version #0.
/// </summary>
[Version(0)]
public sealed class TableInfoDeserialiser0 : IDeserialiser<ITableInfo>
{
   #region Methods
   /// <inheritdoc/>
   public ITableInfo Deserialise(BinaryReader reader)
   {
      int tableSize = reader.Read7BitEncodedInt();
      Dictionary<uint, object?> table = new Dictionary<uint, object?>(tableSize);
      for (int i = 0; i < tableSize; i++)
      {
         uint keyId = reader.ReadUInt32();
         object? value = ReadValue(reader);

         table.Add(keyId, value);
      }

      ITableInfo tableInfo = TableInfoFactory.Version0(table);

      return tableInfo;
   }
   #endregion

   #region Helpers
   private ICollectionInfo ReadCollection(BinaryReader reader)
   {
      int count = reader.Read7BitEncodedInt();
      List<object?> collection = new List<object?>();

      for (int i = 0; i < count; i++)
      {
         object? value = ReadValue(reader);
         collection.Add(value);
      }

      return new CollectionInfo(collection);
   }
   private static IUnknownTableValue ReadUnknown(BinaryReader reader)
   {
      ulong typeId = reader.ReadUInt64();

      return new UnknownTableValue(typeId);
   }
   private object? ReadValue(BinaryReader reader)
   {
      TableDataKind dataKind = (TableDataKind)reader.ReadByte();
      return dataKind switch
      {
         TableDataKind.Null => null,
         TableDataKind.Collection => ReadCollection(reader),
         TableDataKind.Unknown => ReadUnknown(reader),
         TableDataKind.Table => Deserialise(reader),

         _ => ReadPrimitive(reader, dataKind)
      };
   }
   private static object ReadPrimitive(BinaryReader reader, TableDataKind dataKind)
   {
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

      return value;
   }
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