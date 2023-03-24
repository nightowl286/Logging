﻿using System.Diagnostics;
using System.Reflection;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Common.LogData;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Loggers;

namespace TNO.Logging.Writing.Loggers;

/// <summary>
/// Represents a builder for log entries.
/// </summary>
internal class EntryBuilder : IEntryBuilder
{
   #region Fields
   private readonly ILogger _logger;
   private readonly ILogDataCollector _collector;
   private readonly ILogWriteContext _writeContext;
   private readonly Action<IReadOnlyDictionary<ComponentKind, IComponent>> _callback;

   private readonly Dictionary<ComponentKind, IComponent> _components = new Dictionary<ComponentKind, IComponent>();
   #endregion

   #region Constructors
   public EntryBuilder(
      ILogger logger,
      ILogDataCollector collector,
      ILogWriteContext writeContext,
      Action<IReadOnlyDictionary<ComponentKind, IComponent>> callback)
   {
      _logger = logger;
      _collector = collector;
      _writeContext = writeContext;

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
         AssemblyInfo assemblyInfo = AssemblyInfo.FromAssembly(assemblyId, assembly);
         _collector.Deposit(assemblyInfo);
      }

      AssemblyComponent component = new AssemblyComponent(assemblyId);
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
   public IEntryBuilder WithSimple(StackTrace stackTrace, int? threadId = null)
   {
      ThrowIfHasComponent(ComponentKind.SimpleStackTrace);

      threadId ??= Environment.CurrentManagedThreadId;
      string stackTraceStr = stackTrace.ToString();

      SimpleStackTraceComponent component = new SimpleStackTraceComponent(stackTraceStr, threadId.Value);

      return AddComponent(component);
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
