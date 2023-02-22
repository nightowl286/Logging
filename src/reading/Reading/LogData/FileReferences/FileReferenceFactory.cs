using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Reading.Abstractions.LogData.FileReferences;

namespace TNO.Logging.Reading.LogData.FileReferences;

/// <summary>
/// A factory that should be used in instances of the <see cref="IFileReferenceDeserialiser"/>.
/// </summary>
internal static class FileReferenceFactory
{
   #region Functions
   public static FileReference Version0(string file, ulong id)
      => new FileReference(file, id);
   #endregion
}