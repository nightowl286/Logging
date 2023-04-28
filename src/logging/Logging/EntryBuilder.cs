using System.Diagnostics;
using System.Reflection;
using TNO.Logging.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Common.LogData;
using TNO.Logging.Logging.Helpers;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Exceptions;

namespace TNO.Logging.Logging;

/// <summary>
/// Represents a builder for log entries.
/// </summary>
internal class EntryBuilder : IEntryBuilder
{
   #region Fields
   private readonly ILogger _logger;
   private readonly ILogDataCollector _collector;
   private readonly ILogWriteContext _writeContext;
   private readonly IExceptionInfoHandler _exceptionInfoHandler;
   private readonly Action<IReadOnlyDictionary<ComponentKind, IComponent>> _callback;

   private readonly Dictionary<ComponentKind, IComponent> _components = new Dictionary<ComponentKind, IComponent>();
   #endregion

   #region Constructors
   public EntryBuilder(
      ILogger logger,
      ILogDataCollector collector,
      ILogWriteContext writeContext,
      IExceptionInfoHandler exceptionInfoHandler,
      Action<IReadOnlyDictionary<ComponentKind, IComponent>> callback)
   {
      _logger = logger;
      _collector = collector;
      _writeContext = writeContext;
      _exceptionInfoHandler = exceptionInfoHandler;

      _callback = callback;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IEntryBuilder With(string message)
   {
      ThrowIfHasComponent(ComponentKind.Message);

      MessageComponent component = new MessageComponent(message);
      return AddComponent(component);
   }

   /// <inheritdoc/>
   public IEntryBuilder WithTag(string tag)
   {
      ThrowIfHasComponent(ComponentKind.Tag);

      if (_writeContext.GetOrCreateTagId(tag, out ulong tagId))
      {
         TagReference reference = new TagReference(tag, tagId);
         _collector.Deposit(reference);
      }

      TagComponent component = new TagComponent(tagId);
      return AddComponent(component);
   }

   /// <inheritdoc/>
   public IEntryBuilder With(Thread thread)
   {
      ThrowIfHasComponent(ComponentKind.Thread);

      ThreadComponent component = ThreadComponent.FromThread(thread);

      return AddComponent(component);
   }

   /// <inheritdoc/>
   public IEntryBuilder With(ulong entryIdToLink)
   {
      ThrowIfHasComponent(ComponentKind.EntryLink);

      EntryLinkComponent component = new EntryLinkComponent(entryIdToLink);

      return AddComponent(component);
   }

   /// <inheritdoc/>
   public IEntryBuilder With(Assembly assembly)
   {
      ThrowIfHasComponent(ComponentKind.Assembly);

      AssemblyIdentity identity = new AssemblyIdentity(assembly);
      if (_writeContext.GetOrCreateAssemblyId(identity, out ulong assemblyId))
      {
         AssemblyInfo assemblyInfo = AssemblyInfo.FromAssembly(assembly);
         AssemblyReference assemblyReference = new AssemblyReference(assemblyInfo, assemblyId);

         _collector.Deposit(assemblyReference);
      }

      AssemblyComponent component = new AssemblyComponent(assemblyId);
      return AddComponent(component);
   }

   /// <inheritdoc/>
   public IEntryBuilder With(StackTrace stackTrace, int? threadId = null)
   {
      ThrowIfHasComponent(ComponentKind.StackTrace);

      threadId ??= -1;

      IStackTraceInfo stackTraceInfo = StackTraceInfoHelper.GetStackTraceInfo(_writeContext, _collector, stackTrace, threadId.Value);
      StackTraceComponent component = new StackTraceComponent(stackTraceInfo);

      return AddComponent(component);
   }

   /// <inheritdoc/>
   public IEntryBuilder With(Type type)
   {
      ThrowIfHasComponent(ComponentKind.Type);

      ulong typeId = TypeInfoHelper.EnsureIdsForAssociatedTypes(_writeContext, _collector, type);
      TypeComponent component = new TypeComponent(typeId);
      return AddComponent(component);
   }

   /// <inheritdoc/>
   public IEntryBuilder With(Exception exception, int? threadId = null)
   {
      ThrowIfHasComponent(ComponentKind.Exception);

      threadId ??= -1;

      IExceptionInfo exceptionInfo = _exceptionInfoHandler.Convert(exception, threadId);
      ExceptionComponent component = new ExceptionComponent(exceptionInfo);
      return AddComponent(component);
   }

   /// <inheritdoc/>
   public ITableComponentBuilder<IEntryBuilder> WithTable()
   {
      ThrowIfHasComponent(ComponentKind.Table);

      void AddTable(ITableComponent component)
      {
         AddComponent(component);
      }

      TableComponentBuilder<IEntryBuilder> builder = new TableComponentBuilder<IEntryBuilder>(this, _writeContext, _collector, AddTable);
      return builder;
   }

   /// <inheritdoc/>
   public ILogger FinishEntry()
   {
      _callback.Invoke(_components);

      return _logger;
   }

   #endregion

   #region Helpers
   private IEntryBuilder AddComponent(IComponent component)
   {
      _components.Add(component.Kind, component);
      return this;
   }
   private void ThrowIfHasComponent(ComponentKind kind)
   {
      if (_components.ContainsKey(kind))
         throw new InvalidOperationException($"This builder already has the component {kind}.");
   }
   #endregion
}
