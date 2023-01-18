using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
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
      ComponentBase component = ComponentFactory.Message(message);

      AddEntry(severity, entryId, file, line, component);
      return this;
   }
   public ILogger Log(Severity severity, StackFrame stackFrame, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      ComponentBase component = ComponentFactory.StackFrame(stackFrame);

      AddEntry(severity, entryId, file, line, component);
      return this;
   }
   public ILogger Log(Severity severity, StackTrace stackTrace, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      ComponentBase component = ComponentFactory.StackTrace(stackTrace);

      AddEntry(severity, entryId, file, line, component);
      return this;
   }
   public ILogger Log(Severity severity, Exception exception, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      ComponentBase exceptionComponent = ComponentFactory.Exception(exception);

      StackTrace trace = new StackTrace(exception);
      ComponentBase stackTraceException = ComponentFactory.StackTrace(trace);

      AddEntry(severity, entryId, file, line, exceptionComponent, stackTraceException);
      return this;
   }
   public ILogger Log(Severity severity, Thread thread, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      ComponentBase component = ComponentFactory.Thread(thread);

      AddEntry(severity, entryId, file, line, component);
      return this;
   }
   public ILogger Log(Severity severity, Assembly assembly, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      ComponentBase component = ComponentFactory.Assembly(assembly);

      AddEntry(severity, entryId, file, line, component);
      return this;
   }
   public ILogger LogAdditionalPath(Severity severity, string filePath, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0)
   {
      entryId = _mainLogger.RequestEntryId();
      ComponentBase component = ComponentFactory.AdditionalFile(filePath);

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
   private void AddEntry(Severity severity, ulong entryId, string file, int line, params ComponentBase[] components)
   {
      ulong fileRef = _mainLogger.GetFileRef(file);

      LogEntry entry = new LogEntry(_contextId, fileRef, line, entryId, severity, components);

      _mainLogger.AddEntry(entry);
   }
   #endregion
}
