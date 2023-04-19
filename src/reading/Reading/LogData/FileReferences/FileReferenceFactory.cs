using TNO.Logging.Common.Abstractions.LogData;

namespace TNO.Logging.Reading.LogData.FileReferences;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="FileReference"/>.
/// </summary>
internal static class FileReferenceFactory
{
   #region Functions
   public static FileReference Version0(string file, ulong id)
      => new FileReference(file, id);
   #endregion
}