using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData;

namespace TNO.Logging.Writing.Serialisers.LogData;

/// <summary>
/// A serialiser for <see cref="FileReference"/>.
/// </summary>
[Version(0)]
public class FileReferenceSerialiser : IFileReferenceSerialiser
{
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