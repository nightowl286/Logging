using TNO.Common.Abstractions;

namespace TNO.Logging.Writer.Entries.Components;
internal abstract class ComponentBase
{
   #region Properties
   public abstract ComponentKind Kind { get; }
   #endregion
}
