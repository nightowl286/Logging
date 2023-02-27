using System.Text;

namespace TNO.Logging.Writing.Serialisers;

internal static class BinaryWriterSizeHelper
{
   #region Fields
   public static readonly Encoding Encoding = Encoding.UTF8;
   #endregion

   #region Functions
   public static int StringSize(string value)
   {
      // Based on the internal implementation of:
      // BinaryWriter.Write(string)

      int stringSize = Encoding.GetByteCount(value);
      int stringSizeSize = Encoded7BitIntSize(stringSize);

      return stringSize + stringSizeSize;
   }

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
