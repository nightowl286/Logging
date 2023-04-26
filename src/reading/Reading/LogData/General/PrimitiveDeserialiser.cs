using TNO.Logging.Common.Abstractions.LogData.General.Primitives;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.General;

/// <summary>
/// A deserialiser for primitive values.
/// </summary>
public sealed class PrimitiveDeserialiser : IPrimitiveDeserialiser
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="PrimitiveDeserialiser"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>
   public PrimitiveDeserialiser(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public object? Deserialise(BinaryReader reader)
   {
      byte rawKind = reader.ReadByte();
      PrimitiveKind kind = (PrimitiveKind)rawKind;

      return kind switch
      {
         PrimitiveKind.Null => null,
         PrimitiveKind.Unknown => ReadUnknown(reader),

         PrimitiveKind.Table => _deserialiser.Deserialise<ITableInfo>(reader),
         PrimitiveKind.Collection => _deserialiser.Deserialise<ICollectionInfo>(reader),

         PrimitiveKind.Byte => reader.ReadByte(),
         PrimitiveKind.SByte => reader.ReadSByte(),
         PrimitiveKind.UShort => reader.ReadUInt16(),
         PrimitiveKind.Short => reader.ReadInt16(),
         PrimitiveKind.UInt => reader.ReadUInt32(),
         PrimitiveKind.Int => reader.ReadInt32(),
         PrimitiveKind.ULong => reader.ReadUInt64(),
         PrimitiveKind.Long => reader.ReadInt64(),

         PrimitiveKind.Float => reader.ReadSingle(),
         PrimitiveKind.Double => reader.ReadDouble(),
         PrimitiveKind.Decimal => reader.ReadDecimal(),

         PrimitiveKind.Char => reader.ReadChar(),
         PrimitiveKind.String => reader.ReadString(),
         PrimitiveKind.Bool => reader.ReadBoolean(),

         PrimitiveKind.TimeSpan => ReadTimeSpan(reader),
         PrimitiveKind.DateTime => ReadDateTime(reader),
         PrimitiveKind.DateTimeOffset => ReadDateTimeOffset(reader),
         PrimitiveKind.TimeZoneInfo => ReadTimeZoneInfo(reader),

         _ => throw new InvalidDataException($"Unknown primitive kind ({kind}).")
      };
   }
   #endregion

   #region Helpers
   private static IUnknownPrimitive ReadUnknown(BinaryReader reader)
   {
      ulong typeId = reader.ReadUInt64();

      return new UnknownPrimitive(typeId);
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
