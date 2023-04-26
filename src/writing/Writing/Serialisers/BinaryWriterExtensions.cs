using System.Diagnostics.CodeAnalysis;

namespace TNO.Logging.Writing.Serialisers;

/// <summary>
/// Contains useful extension methods related to the <see cref="BinaryWriter"/>.
/// </summary>
public static class BinaryWriterExtensions
{
   #region Methods
   /// <summary>
   /// Writes a <see cref="bool"/> value that indicates if the 
   /// given <paramref name="value"/> is <see langword="null"/>.
   /// </summary>
   /// <typeparam name="T">The type of the <paramref name="value"/>.</typeparam>
   /// <param name="writer">The <see cref="BinaryWriter"/> to use.</param>
   /// <param name="value">The value to check.</param>
   /// <returns>
   /// <see langword="false"/> if the <paramref name="value"/> 
   /// is <see langword="null"/>, otherwise <see langword="true"/>.
   /// </returns>
   public static bool TryWriteNullable<T>(this BinaryWriter writer, [NotNullWhen(true)] T? value)
   {
      if (value is null)
      {
         writer.Write(false);
         return false;
      }

      writer.Write(true);
      return true;
   }

   /// <summary>Writes the given <paramref name="guid"/> to the underlying stream.</summary>
   /// <param name="writer">The writer to use.</param>
   /// <param name="guid">The guid to write.</param>
   public static void Write(this BinaryWriter writer, Guid guid)
   {
      byte[] guidBytes = guid.ToByteArray();
      writer.Write(guidBytes);
   }

#if !NET6_0_OR_GREATER
   /// <summary>Writes a 32-bit integer in a compressed format.</summary>
   /// <param name="writer">The writer to use.</param>
   /// <param name="value">The 32-bit integer to be written.</param>
   public static void Write7BitEncodedInt(this BinaryWriter writer, int value)
   {
      // Implementation copied from; BinaryWriter.Write7BitEncodedInt;
      // Was only made public in NET6

      uint uValue = (uint)value;

      while (uValue > 0x7Fu)
      {
         writer.Write((byte)(uValue | ~0x7Fu));
         uValue >>= 7;
      }

      writer.Write((byte)uValue);
   }
#endif
   #endregion
}
