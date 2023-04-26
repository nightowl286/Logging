using System;
using TNO.Logging.Common.Abstractions.LogData.Primitives;

namespace TNO.Logging.Common.Abstractions.LogData.General.Primitives;

/// <summary>
/// The different value types of primitive values.
/// </summary>
public enum PrimitiveKind : byte
{
   /// <summary>An <see cref="IUnknownPrimitive"/> value.</summary>
   Unknown,

   /// <summary>A <see langword="null"/> value.</summary>
   Null,

   /// <summary>An <see cref="ITableInfo"/> value.</summary>
   Table,

   /// <summary>An <see cref="ICollectionInfo"/> value.</summary>
   Collection,

   #region Numeric
   /// <summary>A <see cref="byte"/> value.</summary>
   [Primitive(typeof(byte), sizeof(byte))]
   Byte,

   /// <summary>An <see cref="sbyte"/> value.</summary>
   [Primitive(typeof(sbyte), sizeof(sbyte))]
   SByte,

   /// <summary>A <see cref="ushort"/> value.</summary>
   [Primitive(typeof(ushort), sizeof(ushort))]
   UShort,

   /// <summary>A <see cref="short"/> value.</summary>
   [Primitive(typeof(short), sizeof(short))]
   Short,

   /// <summary>A <see cref="uint"/> value.</summary>
   [Primitive(typeof(uint), sizeof(uint))]
   UInt,

   /// <summary>An <see cref="int"/> value.</summary>
   [Primitive(typeof(int), sizeof(int))]
   Int,

   /// <summary>A <see cref="ulong"/> value.</summary>
   [Primitive(typeof(ulong), sizeof(ulong))]
   ULong,

   /// <summary>A <see cref="long"/> value.</summary>
   [Primitive(typeof(long), sizeof(long))]
   Long,

   /// <summary>A <see cref="float"/> value.</summary>
   [Primitive(typeof(float), sizeof(float))]
   Float,

   /// <summary>A <see cref="double"/> value.</summary>
   [Primitive(typeof(double), sizeof(double))]
   Double,

   /// <summary>A <see cref="decimal"/> value.</summary>
   [Primitive(typeof(decimal), sizeof(decimal))]
   Decimal,
   #endregion

   #region Text
   /// <summary>A <see cref="char"/> value.</summary>
   [Primitive(typeof(char), sizeof(char))]
   Char,

   /// <summary>A <see cref="string"/> value.</summary>
   [Primitive(typeof(string))]
   String,

   /// <summary>A <see cref="bool"/> value.</summary>
   [Primitive(typeof(bool), sizeof(bool))]
   Bool,
   #endregion

   #region Time
   /// <summary>A <see cref="System.TimeSpan"/> value.</summary>
   [Primitive(typeof(TimeSpan), sizeof(long))]
   TimeSpan,

   /// <summary>A <see cref="System.DateTime"/> value.</summary>
   [Primitive(typeof(DateTime), sizeof(long))]
   DateTime,

   /// <summary>A <see cref="System.DateTimeOffset"/> value.</summary>
   [Primitive(typeof(DateTimeOffset), sizeof(long) * 2)]
   DateTimeOffset,

   /// <summary>A <see cref="System.TimeZoneInfo"/> value.</summary>
   TimeZoneInfo,
   #endregion
}
