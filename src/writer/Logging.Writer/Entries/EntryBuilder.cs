using System.Diagnostics;
using System.Reflection;
using TNO.Common.Abstractions;
using TNO.Logging.Writer.Abstractions;
using TNO.Logging.Writer.Entries.Components;

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
   private readonly Dictionary<ComponentKind, ComponentBase> _components = new Dictionary<ComponentKind, ComponentBase>();
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
      ComponentBase component = ComponentFactory.Message(message);
      return AddComponent(component);
   }
   public ILogEntryBuilder With(StackFrame stackFrame)
   {
      ComponentBase component = ComponentFactory.StackFrame(stackFrame);
      return AddComponent(component);
   }
   public ILogEntryBuilder With(StackTrace stackTrace)
   {
      ComponentBase component = ComponentFactory.StackTrace(stackTrace);
      return AddComponent(component);
   }
   public ILogEntryBuilder With(Exception exception)
   {
      ComponentBase component = ComponentFactory.Exception(exception);
      return AddComponent(component);
   }
   public ILogEntryBuilder With(Thread thread)
   {
      ComponentBase component = ComponentFactory.Thread(thread);
      return AddComponent(component);
   }
   public ILogEntryBuilder With(Assembly assembly)
   {
      ComponentBase component = ComponentFactory.Assembly(assembly);
      return AddComponent(component);
   }
   public ILogEntryBuilder With(ulong entryIdToLink)
   {
      ComponentBase component = ComponentFactory.EntryLink(entryIdToLink);
      return AddComponent(component);
   }
   public ILogEntryBuilder WithAdditionalFile(string path)
   {
      ComponentBase component = ComponentFactory.AdditionalFile(path);
      return AddComponent(component);
   }
   public ILogEntryBuilder WithTag(string tag)
   {
      ulong tagId = _mainLogger.GetTagId(tag);
      ComponentBase component = ComponentFactory.Tag(tagId);
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
   private ILogEntryBuilder AddComponent(ComponentBase component)
   {
      if (_components.TryAdd(component.Kind, component))
         return this;

      // Todo(Nightowl): Improve this message;
      throw new ArgumentException($"A component of the given kind ({component.Kind}) has already been added to this entry.", nameof(component));
   }
   #endregion
}
