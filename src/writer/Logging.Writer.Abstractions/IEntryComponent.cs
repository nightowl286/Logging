using TNO.Common.Abstractions;

namespace TNO.Logging.Writer.Abstractions;
public interface IEntryComponent
{
   #region Properties
   ComponentKind Kind { get; }
   #endregion
}
