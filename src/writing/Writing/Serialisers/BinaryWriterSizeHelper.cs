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
