using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using TNO.Common.Abstractions;
using TNO.Common.Abstractions.Components;
using TNO.Logging.Writer.Abstractions;
using TNO.Logging.Writer.Entries;
using TNO.Logging.Writer.Entries.Components;

namespace TNO.Logging.Writer.Loggers;
internal class ContextLogger : ILogger
{
   #region Fields
   private readonly ulong _contextId;
   private readonly MainLogger _mainLogger;
   #endregion
   public ContextLogger(MainLogger mainLogger, ulong contextId)
   {
      _mainLogger = mainLogger;
      _contextId = contextId;
   }

   #region Methods
   public ILogger Log(Severity severity, string message, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      IEntryComponent component = ComponentFactory.Message(message);

      AddEntry(severity, entryId, file, line, component);
      return this;
   }
   public ILogger Log(Severity severity, StackFrame stackFrame, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      IEntryComponent component = ComponentFactory.StackFrame(stackFrame);

      AddEntry(severity, entryId, file, line, component);
      return this;
   }
   public ILogger Log(Severity severity, StackTrace stackTrace, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      IEntryComponent component = ComponentFactory.StackTrace(stackTrace);

      AddEntry(severity, entryId, file, line, component);
      return this;
   }
   public ILogger Log(Severity severity, Exception exception, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      IEntryComponent exceptionComponent = ComponentFactory.Exception(exception);

      StackTrace trace = new StackTrace(exception);
      IEntryComponent stackTraceComponent = ComponentFactory.StackTrace(trace);

      Thread thread = Thread.CurrentThread;
      IEntryComponent threadComponent = ComponentFactory.Thread(thread);

      AddEntry(severity, entryId, file, line, exceptionComponent, stackTraceComponent, threadComponent);
      return this;
   }
   public ILogger Log(Severity severity, Thread thread, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      IEntryComponent component = ComponentFactory.Thread(thread);

      AddEntry(severity, entryId, file, line, component);
      return this;
   }
   public ILogger Log(Severity severity, Assembly assembly, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      IEntryComponent component = ComponentFactory.Assembly(assembly);

      AddEntry(severity, entryId, file, line, component);
      return this;
   }
   public ILogger LogAdditionalPath(Severity severity, string filePath, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      IEntryComponent component = ComponentFactory.AdditionalFile(filePath);

      AddEntry(severity, entryId, file, line, component);
      return this;
   }
   public ILogEntryBuilder StartEntry(Severity severity, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      ulong fileRef = _mainLogger.GetFileRef(file);

      EntryBuilder builder = new EntryBuilder(_mainLogger, this, _contextId, entryId, fileRef, line, severity);

      return builder;
   }
   public ILogger CreateLinks(ulong[] entryIds, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      _mainLogger.AddLinks(_contextId, file, line, entryIds);

      return this;
   }
   public ILogger CreateContext(string name) => _mainLogger.CreateContext(name, _contextId);
   #endregion

   #region Helpers
   private void AddEntry(Severity severity, ulong entryId, string file, int line, params IEntryComponent[] components)
   {
      ulong fileRef = _mainLogger.GetFileRef(file);

      LogEntry entry = new LogEntry(_contextId, fileRef, line, entryId, severity, components);

      _mainLogger.AddEntry(entry);
   }
   #endregion
}
