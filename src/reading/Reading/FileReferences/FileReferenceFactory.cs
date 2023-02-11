using TNO.Logging.Common.Abstractions;
using TNO.Logging.Reading.Abstractions.FileReferences;

namespace TNO.Logging.Reading.FileReferences;

/// <summary>
/// A factory that should be used in instances of the see <see cref="IFileReferenceDeserialiser"/>.
/// </summary>
internal static class FileReferenceFactory
{
   #region Functions
   public static FileReference Version0(string file, ulong id)
      => new FileReference(file, id);
   #endregion
}
