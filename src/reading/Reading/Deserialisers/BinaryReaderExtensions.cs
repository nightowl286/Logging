namespace TNO.Logging.Reading.Deserialisers;

/// <summary>
/// Contains useful extension methods related to the <see cref="BinaryReader"/>.
/// </summary>
public static class BinaryReaderExtensions
{
   /// <summary>Reads a nullable value from the underlying stream.</summary>
   /// <typeparam name="T">The type of the value to read.</typeparam>
   /// <param name="reader">The <see cref="BinaryReader"/> to use.</param>
   /// <param name="readFunction">The function that will read a non-null version of the <typeparamref name="T"/>.</param>
   /// <returns>The value read by the given <paramref name="readFunction"/>, or <see langword="null"/>.</returns>
   public static T? TryReadNullable<T>(this BinaryReader reader, Func<T> readFunction)
   {
      if (reader.ReadBoolean())
         return readFunction.Invoke();

      return default;
   }

   /// <summary>Reads a <see cref="Guid"/> from the underlying stream.</summary>
   /// <param name="reader">The <see cref="BinaryReader"/> to use.</param>
   /// <returns>The read <see cref="Guid"/>.</returns>
   public static Guid ReadGuid(this BinaryReader reader)
   {
      const int guidSize = 16;

      Span<byte> bytes = stackalloc byte[guidSize];
      reader.Read(bytes);

      return new Guid(bytes);
   }
}
