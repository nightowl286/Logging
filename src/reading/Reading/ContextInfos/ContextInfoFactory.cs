using TNO.Logging.Common.Abstractions;
using TNO.Logging.Reading.Abstractions.ContextInfos;

namespace TNO.Logging.Reading.ContextInfos;

/// <summary>
/// A factory that should be used in instances of the see <see cref="IContextInfoDeserialiser"/>.
/// </summary>
internal static class ContextInfoFactory
{
   #region Functions
   public static ContextInfo Version0(string name, ulong id, ulong fileId, uint lineInFile)
      => new ContextInfo(name, id, fileId, lineInFile);
   #endregion
}
