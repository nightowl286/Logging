using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Abstractions.LogData.Tables;

/// <summary>
/// The different value types that a <see cref="ITableComponent"/> can contain.
/// </summary>
public enum TableDataKind : byte
{
   /// <summary>An <see cref="IUnknownTableValue"/> value.</summary>
   Unknown,

   /// <summary>A <see langword="null"/> value.</summary>
   Null,

   #region Numeric
   /// <summary>A <see cref="byte"/> value.</summary>
   Byte,

   /// <summary>An <see cref="sbyte"/> value.</summary>
   SByte,

   /// <summary>A <see cref="ushort"/> value.</summary>
   UShort,

   /// <summary>A <see cref="short"/> value.</summary>
   Short,

   /// <summary>A <see cref="uint"/> value.</summary>
   UInt,

   /// <summary>An <see cref="int"/> value.</summary>
   Int,

   /// <summary>A <see cref="ulong"/> value.</summary>
   ULong,

   /// <summary>A <see cref="long"/> value.</summary>
   Long,

   /// <summary>A <see cref="float"/> value.</summary>
   Float,

   /// <summary>A <see cref="double"/> value.</summary>
   Double,

   /// <summary>A <see cref="decimal"/> value.</summary>
   Decimal,
   #endregion

   #region Text
   /// <summary>A <see cref="char"/> value.</summary>
   Char,

   /// <summary>A <see cref="string"/> value.</summary>
   String,

   /// <summary>A <see cref="bool"/> value.</summary>
   Bool,
   #endregion

   #region Time
   /// <summary>A <see cref="System.TimeSpan"/> value.</summary>
   TimeSpan,

   /// <summary>A <see cref="System.DateTime"/> value.</summary>
   DateTime,

   /// <summary>A <see cref="System.DateTimeOffset"/> value.</summary>
   DateTimeOffset,

   /// <summary>A <see cref="System.TimeZoneInfo"/> value.</summary>
   TimeZoneInfo,
   #endregion

   /// <summary>A <see cref="ITableInfo"/> value.</summary>
   Table,

   /// <summary>A <see cref="ICollectionInfo"/> value.</summary>
   Collection,
}
