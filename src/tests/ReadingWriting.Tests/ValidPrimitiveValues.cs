namespace TNO.ReadingWriting.Tests;

public static class ValidPrimitiveValues
{
   public static object?[] Values { get; } = new object?[]
   {
      (byte)1, (sbyte)2, (ushort)3, (short)4, 5u, 6, 7uL, 8L, // integer
      8.0f, 9.0d, 10.0m, // floating
      't', (char)0x7FFu, (char)0xFFFFu, // chars
      "test",
      true, false, // bool
      TimeSpan.FromHours(3), DateTime.Now, DateTimeOffset.Now, // time
      TimeZoneInfo.Utc, TimeZoneInfo.Local, // time zone
      //null,
      //new UnknownPrimitive(1),
   };

   public static IEnumerable<object?[]> AsArguments()
   {
      foreach (object? value in Values)
         yield return new[] { value };
   }
}