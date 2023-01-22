using System.Diagnostics;
using TNO.Common.Abstractions.Components;
using TNO.Common.Abstractions.Components.Kinds;

namespace TNO.Logging.Writer.Entries.Components.Kinds;

internal record SimpleStackFrameComponent(string StackFrame) : ISimpleStackFrameComponent
{
   #region Properties
   public ComponentKind Kind => ComponentKind.SimpleStackFrame;
   #endregion

   #region Functions
   public static SimpleStackFrameComponent FromStackFrame(StackFrame frame)
   {
      string str = frame.ToString();
      return new SimpleStackFrameComponent(str);
   }
   #endregion
}
