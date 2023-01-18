using System.Collections.Generic;

namespace TNO.Common.Abstractions.Components.Kinds;
public interface IStackTraceComponent : IEntryComponent
{
   #region Properties
   IReadOnlyList<IStackFrameInfo> Frames { get; }
   int ThreadId { get; }
   #endregion
}
