using System.Diagnostics;
using System.Reflection;
using TNO.Common.Abstractions.Components;
using TNO.Logging.Writer.Entries.Components.Kinds;

namespace TNO.Logging.Writer.Entries.Components;
internal static class ComponentFactory
{
   #region Functions
   public static IEntryComponent Message(string message) => new MessageComponent(message);
   public static IEntryComponent StackFrame(StackFrame frame) => SimpleStackFrame(frame);
   public static IEntryComponent StackTrace(StackTrace trace) => SimpleStackTrace(trace);
   public static IEntryComponent Exception(Exception exception) => throw new NotImplementedException();
   public static IEntryComponent Thread(Thread thread) => ThreadComponent.FromThread(thread);
   public static IEntryComponent Assembly(Assembly assembly) => throw new NotImplementedException();
   public static IEntryComponent EntryLink(ulong entryId) => new LinkComponent(entryId);
   public static IEntryComponent AdditionalFile(string path) => throw new NotImplementedException();
   public static IEntryComponent Tag(ulong tagId) => new TagComponent(tagId);
   public static IEntryComponent SimpleStackFrame(StackFrame frame) => SimpleStackFrameComponent.FromStackFrame(frame);
   public static IEntryComponent SimpleStackTrace(StackTrace trace) => SimpleStackTraceComponent.FromStackTrace(trace);
   #endregion
}
