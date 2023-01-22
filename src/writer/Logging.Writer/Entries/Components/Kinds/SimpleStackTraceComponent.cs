using System.Diagnostics;
using TNO.Common.Abstractions.Components;
using TNO.Common.Abstractions.Components.Kinds;

namespace TNO.Logging.Writer.Entries.Components.Kinds;

internal record SimpleStackTraceComponent(string StackTrace) : ISimpleStackTraceComponent
{
   #region Properties
   public ComponentKind Kind => ComponentKind.SimpleStackTrace;
   #endregion

   #region Functions
   public static SimpleStackTraceComponent FromStackTrace(StackTrace trace)
   {
      string str = trace.ToString();
      return new SimpleStackTraceComponent(str);
   }
   #endregion
}
