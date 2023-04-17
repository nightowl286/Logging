namespace TNO.Logging.Reading.Deserialisers;

/// <summary>
/// Contains useful extension methods related to the <see cref="BinaryReader"/>.
/// </summary>
public static class BinaryReaderExtensions
{
   public static T? TryReadNullable<T>(this BinaryReader reader, Func<T> readFunction)
   {
      if (reader.ReadBoolean())
         return readFunction.Invoke();

      return default;
   }

   public static Guid ReadGuid(this BinaryReader reader)
   {
      const int guidSize = 16;

      Span<byte> bytes = stackalloc byte[guidSize];
      reader.Read(bytes);

      return new Guid(bytes);
   }
}
