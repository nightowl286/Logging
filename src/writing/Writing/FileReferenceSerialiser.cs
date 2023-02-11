using TNO.Logging.Common.Abstractions;
using TNO.Logging.Writing.Abstractions;

namespace TNO.Logging.Writing;

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
   #endregion
}
