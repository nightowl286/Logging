using TNO.Common.Abstractions;
using TNO.Logging.Writer.Abstractions;

namespace TNO.Logging.Writer.Entries.Components;
internal abstract class ComponentBase : IEntryComponent
{
   #region Properties
   public abstract ComponentKind Kind { get; }
   #endregion
}
