using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.FileReferences.Versions;

/// <summary>
/// A deserialiser for <see cref="FileReference"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.FileReference)]
public sealed class FileReferenceDeserialiser0 : IDeserialiser<FileReference>
{
   #region Methods
   /// <inheritdoc/>
   public FileReference Deserialise(BinaryReader reader)
   {
      string file = reader.ReadString();
      ulong id = reader.ReadUInt64();

      return FileReferenceFactory.Version0(file, id);
   }
   #endregion
}