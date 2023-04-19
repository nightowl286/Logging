using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData;

/// <summary>
/// A serialiser for <see cref="FileReference"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.FileReference)]
public class FileReferenceSerialiser : ISerialiser<FileReference>
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