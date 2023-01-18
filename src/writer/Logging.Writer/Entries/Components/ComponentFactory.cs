using System.Diagnostics;
using System.Reflection;
using TNO.Common.Abstractions.Components;
using TNO.Logging.Writer.Entries.Components.Kinds;

namespace TNO.Logging.Writer.Entries.Components;
internal static class ComponentFactory
{
   #region Functions
   public static IEntryComponent Message(string message) => new MessageComponent(message);
   public static IEntryComponent StackFrame(StackFrame frame) => throw new NotImplementedException();
   public static IEntryComponent StackTrace(StackTrace trace) => throw new NotImplementedException();
   public static IEntryComponent Exception(Exception exception) => throw new NotImplementedException();
   public static IEntryComponent Thread(Thread thread) => throw new NotImplementedException();
   public static IEntryComponent Assembly(Assembly assembly) => throw new NotImplementedException();
   public static IEntryComponent EntryLink(ulong entryId) => throw new NotImplementedException();
   public static IEntryComponent AdditionalFile(string path) => throw new NotImplementedException();
   public static IEntryComponent Tag(ulong tagId) => throw new NotImplementedException();
   #endregion
}
