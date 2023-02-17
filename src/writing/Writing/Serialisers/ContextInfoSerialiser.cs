using TNO.Logging.Common.Abstractions;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers;

/// <summary>
/// A serialiser for <see cref="ContextInfo"/>.
/// </summary>
public class ContextInfoSerialiser : IContextInfoSerialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

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
