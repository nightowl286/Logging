using System.Reflection;
using TNO.Logging.Common.LogData.Tables;

namespace TNO.ReadingWriting.Tests;

public static class ValidPrimitiveValues
{
   public static Annotated<object?>[] Values { get; } = new Annotated<object?>[]
   {
      // integers
      new((byte)1),
      new((sbyte)2),
      new((ushort)3),
      new((short)4),
      new(5u),
      new(6),
      new(7uL),
      new(8L),

      // floating
      new(8.0f),
      new(9.0d),
      new(10.0m),

      // chars
      new('t'),
      new((char)0x7FFu),
      new((char)0xFFFFu),

      // boolean
      new(true),
      new(false),

      // time
      new(TimeSpan.FromHours(3)),
      new(DateTime.Now),
      new(DateTimeOffset.Now),

      // time zone
      new(TimeZoneInfo.Utc),
      new(TimeZoneInfo.Local),

      // other
      new("test"),
      new(null),
      new(new UnknownPrimitive(1)),
   };

   public static IEnumerable<object[]> AsArguments()
   {
      foreach (Annotated value in Values)
         yield return new object[] { value };
   }

   public static string GetDisplayName(MethodInfo _, object?[] values)
   {
      string annotation = ((Annotated?)values[0])?.Annotation ?? "<null>";

      return annotation;
   }
}