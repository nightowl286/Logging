using System.Diagnostics;
using System.Reflection;

namespace TNO.Logging.Writer.Entries.Components;
internal static class ComponentFactory
{
   #region Functions
   public static ComponentBase Message(string message) => throw new NotImplementedException();
   public static ComponentBase StackFrame(StackFrame frame) => throw new NotImplementedException();
   public static ComponentBase StackTrace(StackTrace trace) => throw new NotImplementedException();
   public static ComponentBase Exception(Exception exception) => throw new NotImplementedException();
   public static ComponentBase Thread(Thread thread) => throw new NotImplementedException();
   public static ComponentBase Assembly(Assembly assembly) => throw new NotImplementedException();
   public static ComponentBase EntryLink(ulong entryId) => throw new NotImplementedException();
   public static ComponentBase AdditionalFile(string path) => throw new NotImplementedException();
   public static ComponentBase Tag(ulong tagId) => throw new NotImplementedException();
   #endregion
}
