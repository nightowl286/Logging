using System.Diagnostics;
using System.Reflection;
using TNO.Common.Abstractions;
using TNO.Common.Abstractions.Components;
using TNO.Logging.Writer.Abstractions;
using TNO.Logging.Writer.Entries.Components;
using TNO.Logging.Writer.Loggers;

namespace TNO.Logging.Writer.Entries;
internal class EntryBuilder : ILogEntryBuilder
{
   #region Fields
   private readonly MainLogger _mainLogger;
   private readonly ILogger _parentLogger;
   private readonly ulong _entryId;
   private readonly ulong _contextId;
   private readonly Severity _severity;
   private readonly ulong _fileRef;
   private readonly int _line;
   private readonly Dictionary<ComponentKind, IEntryComponent> _components = new Dictionary<ComponentKind, IEntryComponent>();
   private bool _stackTraceFromException = false;
   private bool _threadFromException = false;
   #endregion
   public EntryBuilder(MainLogger mainLogger, ILogger parentLogger, ulong contextId, ulong entryId, ulong fileRef, int line, Severity severity)
   {
      _mainLogger = mainLogger;
      _parentLogger = parentLogger;
      _contextId = contextId;
      _entryId = entryId;
      _fileRef = fileRef;
      _line = line;
      _severity = severity;
   }

   #region Methods
   public ILogEntryBuilder With(string message)
   {
      IEntryComponent component = ComponentFactory.Message(message);
      return AddComponent(component);
   }
   public ILogEntryBuilder With(StackFrame stackFrame)
   {
      IEntryComponent component = ComponentFactory.StackFrame(stackFrame);
      AddComponent(component, _stackTraceFromException);

      _stackTraceFromException = false;
      return this;
   }
   public ILogEntryBuilder With(StackTrace stackTrace)
   {
      IEntryComponent component = ComponentFactory.StackTrace(stackTrace);
      AddComponent(component, _threadFromException);

      _threadFromException = false;
      return this;
   }
   public ILogEntryBuilder With(Exception exception)
   {
      IEntryComponent component = ComponentFactory.Exception(exception);
      AddComponent(component);

      if (_components.ContainsKey(ComponentKind.StackTrace) == false)
      {
         StackTrace stackTrace = new StackTrace(exception);
         IEntryComponent stackTraceComponent = ComponentFactory.StackTrace(stackTrace);
         AddComponent(stackTraceComponent);
         _stackTraceFromException = true;
      }

      if (_components.ContainsKey(ComponentKind.Thread) == false)
      {
         Thread thread = Thread.CurrentThread;
         IEntryComponent threadComponent = ComponentFactory.Thread(thread);
         AddComponent(threadComponent);
         _threadFromException = true;
      }

      return this;
   }
   public ILogEntryBuilder With(Thread thread)
   {
      IEntryComponent component = ComponentFactory.Thread(thread);
      return AddComponent(component);
   }
   public ILogEntryBuilder With(Assembly assembly)
   {
      IEntryComponent component = ComponentFactory.Assembly(assembly);
      return AddComponent(component);
   }
   public ILogEntryBuilder With(ulong entryIdToLink)
   {
      IEntryComponent component = ComponentFactory.EntryLink(entryIdToLink);
      return AddComponent(component);
   }
   public ILogEntryBuilder WithAdditionalFile(string path)
   {
      IEntryComponent component = ComponentFactory.AdditionalFile(path);
      return AddComponent(component);
   }
   public ILogEntryBuilder WithTag(string tag)
   {
      ulong tagId = _mainLogger.GetTagId(tag);
      IEntryComponent component = ComponentFactory.Tag(tagId);
      return AddComponent(component);
   }
   public ILogger FinishEntry()
   {
      LogEntry entry = new LogEntry(_contextId, _fileRef, _line, _entryId, _severity, _components.Values);
      _mainLogger.AddEntry(entry);

      return _parentLogger;
   }
   #endregion

   #region Helpers
   private ILogEntryBuilder AddComponent(IEntryComponent component, bool allowReplace = true)
   {
      if (allowReplace)
      {
         _components[component.Kind] = component;
         return this;
      }

      if (_components.TryAdd(component.Kind, component))
         return this;

      // Todo(Nightowl): Improve this message;
      throw new ArgumentException($"A component of the given kind ({component.Kind}) has already been added to this entry.", nameof(component));
   }
   #endregion
}
