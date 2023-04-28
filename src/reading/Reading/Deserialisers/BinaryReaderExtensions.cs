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
   public static T? TryReadNullable<T>(this BinaryReader reader, Func<T> readFunction) where T : class
   {
      if (reader.ReadBoolean())
         return readFunction.Invoke();

      return default;
   }

   /// <inheritdoc cref="TryReadNullable{T}(BinaryReader, Func{T})"/>
   public static T? TryReadNullableStruct<T>(this BinaryReader reader, Func<T> readFunction) where T : struct
   {
      if (reader.ReadBoolean())
         return readFunction.Invoke();

      return null;
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

#if !NET6_0_OR_GREATER
   /// <summary>Reads in a 32-bit integer in a compressed format.</summary>
   /// <param name="reader">The reader to use.</param>
   /// <returns>A 32-bit integer in a compressed format.</returns>
   public static int Read7BitEncodedInt(this BinaryReader reader)
   {
      // Implementation copied from; BinaryReader.Read7BitEncodedInt;
      // Was only made public in NET6

      uint result = 0;
      byte byteReadJustNow;

      const int MaxBytesWithoutOverflow = 4;
      for (int shift = 0; shift < MaxBytesWithoutOverflow * 7; shift += 7)
      {
         byteReadJustNow = reader.ReadByte();
         result |= (byteReadJustNow & 0x7Fu) << shift;

         if (byteReadJustNow <= 0x7Fu)
         {
            return (int)result;
         }
      }

      byteReadJustNow = reader.ReadByte();
      if (byteReadJustNow > 0b_1111u)
      {
         throw new FormatException($"Too many bytes in what should have been a 7 bit encoded Int32.");
      }

      result |= (uint)byteReadJustNow << (MaxBytesWithoutOverflow * 7);
      return (int)result;
   }
#endif
}
