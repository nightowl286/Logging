using TNO.Common.Abstractions.Components;
using TNO.Common.Abstractions.Components.Kinds;

namespace TNO.Logging.Writer.Entries.Components.Kinds;

internal record StackFrameComponent(
   ulong FileReference,
   int Line,
   int Column,
   int ThreadId) : StackFrameInfo(FileReference, Line, Column), IStackFrameComponent
{
   #region Properties
   public ComponentKind Kind => ComponentKind.StackFrame;
   #endregion
}
