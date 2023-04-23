using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData;

/// <summary>
/// A serialiser for <see cref="ContextInfo"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.ContextInfo)]
public class ContextInfoSerialiser : ISerialiser<ContextInfo>
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ContextInfo data)
   {
      ulong id = data.Id;
      ulong parentId = data.ParentId;
      string name = data.Name;
      ulong fileId = data.FileId;
      uint line = data.LineInFile;

      writer.Write(id);
      writer.Write(parentId);
      writer.Write(name);
      writer.Write(fileId);
      writer.Write(line);
   }

   /// <inheritdoc/>
   public int Count(ContextInfo data)
   {
      string name = data.Name;

      int nameSize = BinaryWriterSizeHelper.StringSize(name);

      int size =
         nameSize +
         (sizeof(ulong) * 3) +
         sizeof(uint);

      return size;
   }
   #endregion
}
