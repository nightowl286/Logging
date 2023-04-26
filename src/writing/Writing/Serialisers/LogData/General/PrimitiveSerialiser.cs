using TNO.Logging.Common.Abstractions.LogData.General.Primitives;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData.General;

/// <summary>
/// A serialiser for primitive values.
/// </summary>
public sealed class PrimitiveSerialiser : IPrimitiveSerialiser
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="PrimitiveSerialiser"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public PrimitiveSerialiser(ISerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, object? data)
   {
      if (data is null)
         writer.Write((byte)PrimitiveKind.Null);
      else if (data is string @string)
      {
         writer.Write((byte)PrimitiveKind.String);
         writer.Write(@string);
      }
      else if (data is char @char)
      {
         writer.Write((byte)PrimitiveKind.Char);
         writer.Write(@char);
      }
      else if (data is TimeZoneInfo timeZoneInfo)
      {
         writer.Write((byte)PrimitiveKind.TimeZoneInfo);
         writer.Write(timeZoneInfo.Id);
      }
      else if (data is ITableInfo tableInfo)
      {
         writer.Write((byte)PrimitiveKind.Table);
         _serialiser.Serialise(writer, tableInfo);
      }
      else if (data is ICollectionInfo collectionInfo)
      {
         writer.Write((byte)PrimitiveKind.Collection);
         _serialiser.Serialise(writer, collectionInfo);
      }
      else if (data is IUnknownPrimitive unknownPrimitive)
      {
         writer.Write((byte)PrimitiveKind.Unknown);
         writer.Write(unknownPrimitive.TypeId);
      }
      else
         SetLengthSerialise(writer, data);
   }
   private static void SetLengthSerialise(BinaryWriter writer, object data)
   {
      Type type = data.GetType();
      if (type.TryGetPrimitiveKind(out PrimitiveKind? kind, out _) == false)
         throw new ArgumentException($"Unknown data type ({type}) for the given data ({data}).", nameof(data));

      writer.Write((byte)kind);

      #region Numeric
      if (data is byte @byte) writer.Write(@byte);
      else if (data is sbyte @sbyte) writer.Write(@sbyte);
      else if (data is ushort @ushort) writer.Write(@ushort);
      else if (data is short @short) writer.Write(@short);
      else if (data is uint @uint) writer.Write(@uint);
      else if (data is int @int) writer.Write(@int);
      else if (data is ulong @ulong) writer.Write(@ulong);
      else if (data is long @long) writer.Write(@long);
      else if (data is float @float) writer.Write(@float);
      else if (data is double @double) writer.Write(@double);
      else if (data is decimal @decimal) writer.Write(@decimal);
      #endregion
      else if (data is bool @bool) writer.Write(@bool);
      else if (data is TimeSpan timeSpan) writer.Write(timeSpan.Ticks);
      else if (data is DateTime dateTime) writer.Write(dateTime.Ticks);
      else if (data is DateTimeOffset dateTimeOffset)
      {
         writer.Write(dateTimeOffset.Ticks);
         writer.Write(dateTimeOffset.Offset.Ticks);
      }
      else
         throw new Exception($"The kind ({kind}) has not been properly handled.");
   }

   /// <inheritdoc/>
   public int Count(object? data)
   {
      int size = sizeof(byte); // PrimitiveKind
      return size + data switch
      {
         null => 0,
         string @string => BinaryWriterSizeHelper.StringSize(@string),
         char @char => BinaryWriterSizeHelper.CharSize(@char),
         TimeZoneInfo timeZoneInfo => BinaryWriterSizeHelper.StringSize(timeZoneInfo.Id),
         ITableInfo tableInfo => _serialiser.Count(tableInfo),
         ICollectionInfo collectionInfo => _serialiser.Count(collectionInfo),
         IUnknownPrimitive => sizeof(ulong),

         _ => SetLengthCount(data)
      };
   }

   private static int SetLengthCount(object data)
   {
      Type type = data.GetType();
      if (type.TryGetPrimitiveKind(out PrimitiveKind? kind, out PrimitiveAttribute? attr) == false)
         throw new ArgumentException($"Unknown data type ({type}) for the given data ({data}).", nameof(data));

      if (attr.Size < 0)
         throw new Exception($"The kind ({kind}) has not been properly handled.");

      return attr.Size;
   }
   #endregion
}
