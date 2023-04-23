using System.Text;

namespace TNO.Logging.Writing.Serialisers;

/// <summary>
/// Contains useful functions for calculating the size of data that will be written by a <see cref="BinaryWriter"/>.
/// </summary>
public static class BinaryWriterSizeHelper
{
   #region Constants
   /// <summary>The size (in bytes) of a <see cref="Guid"/>.</summary>
   public const int GuidSize = 16;
   #endregion

   #region Fields
   /// <summary>The assumed encoding.</summary>
   public static readonly Encoding Encoding = Encoding.UTF8;
   #endregion

   #region Functions
   /// <summary>Calculates the length of a written <see cref="string"/> <paramref name="value"/>.</summary>
   /// <remarks><see langword="null"/> values will return a value of <c>0</c>.</remarks>
   public static int StringSize(string? value)
   {
      if (value == null)
         return 0;

      // Based on the internal implementation of:
      // BinaryWriter.Write(string)

      int stringSize = Encoding.GetByteCount(value);
      int stringSizeSize = Encoded7BitIntSize(stringSize);

      return stringSize + stringSizeSize;
   }

   /// <summary>Calculates the length of a written <see cref="char"/> <paramref name="value"/>.</summary>
   /// <param name="value">The <see cref="char"/> value to check.</param>
   public static int CharSize(char value)
   {
      // Based on the internal implementations of:
      // BinaryWriter.Write(char);
      // Rune.TryEncodeToUtf8;

      if (Rune.TryCreate(value, out Rune rune) == false)
         throw new ArgumentException($"Surrogates as a single char value cannot be handled by the ({typeof(BinaryWriter)}).");

      if (rune.IsAscii) return 1;
      if (rune.Value <= 0x7FFu) return 2;
      if (rune.Value <= 0xFFFFu) return 3;
      return 4; // should not be possible as the BinaryWriter cannot handle it.
   }

   /// <summary>Calculates the length that the given <paramref name="value"/> will take encoded as a 7-bit integer.</summary>
   public static int Encoded7BitIntSize(int value)
   {
      // Based on the internal implementations of:
      // BinaryWriter.Write7BitEncodedInt(int)

      uint encoded = (uint)value;

      int size = 1;
      while (encoded > 0x7Fu)
      {
         encoded >>= 7;
         size++;
      }

      return size;
   }
   #endregion
}
