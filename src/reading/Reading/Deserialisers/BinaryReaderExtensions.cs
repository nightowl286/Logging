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
}
