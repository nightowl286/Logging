using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData;

namespace TNO.Logging.Writing.Serialisers.LogData;

/// <summary>
/// A serialiser for <see cref="ContextInfo"/>.
/// </summary>
[Version(0)]
public class ContextInfoSerialiser : IContextInfoSerialiser
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
   public ulong Count(ContextInfo data)
   {
      string name = data.Name;

      int nameSize = BinaryWriterSizeHelper.StringSize(name);

      int size =
         nameSize +
         (sizeof(ulong) * 3) +
         sizeof(uint);

      return (ulong)size;
   }
   #endregion
}
