using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Reading.Abstractions.LogData.ContextInfos;

namespace TNO.Logging.Reading.LogData.ContextInfos;

/// <summary>
/// A factory that should be used in instances of the see <see cref="IContextInfoDeserialiser"/>.
/// </summary>
internal static class ContextInfoFactory
{
   #region Functions
   public static ContextInfo Version0(string name, ulong id, ulong parentId, ulong fileId, uint lineInFile)
      => new ContextInfo(name, id, parentId, fileId, lineInFile);
   #endregion
}
