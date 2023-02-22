using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData;

namespace TNO.Logging.Writing.Serialisers.LogData;

/// <summary>
/// A serialiser for <see cref="FileReference"/>.
/// </summary>
public class FileReferenceSerialiser : IFileReferenceSerialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;

   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, FileReference data)
   {
      string file = data.File;
      ulong id = data.Id;

      writer.Write(file);
      writer.Write(id);
   }

   /// <inheritdoc/>
   public ulong Count(FileReference data)
   {
      string file = data.File;

      int fileSize = BinaryWriterSizeHelper.StringSize(file);

      return (ulong)(fileSize + sizeof(ulong));
   }
   #endregion
}