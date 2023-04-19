using TNO.Logging.Common.Abstractions.LogData;

namespace TNO.Logging.Reading.LogData.ContextInfos;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="ContextInfo"/>.
/// </summary>
internal static class ContextInfoFactory
{
   #region Functions
   public static ContextInfo Version0(string name, ulong id, ulong parentId, ulong fileId, uint lineInFile)
      => new ContextInfo(name, id, parentId, fileId, lineInFile);
   #endregion
}
