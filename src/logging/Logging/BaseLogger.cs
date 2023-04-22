using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using TNO.Logging.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Entries;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Common.LogData;
using TNO.Logging.Logging.Helpers;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Exceptions;

namespace TNO.Logging.Logging;

/// <summary>
/// Represents the very base logger.
/// </summary>
public class BaseLogger : ILogger
{
   #region Fields
   private readonly ulong _scope;
   #endregion

   #region Properties
   /// <summary>The collector that should receive the log data.</summary>
   protected ILogDataCollector Collector { get; }

   /// <summary>The write context used by this logger.</summary>
   protected ILogWriteContext WriteContext { get; }

   /// <summary>The exception info handler used by this logger.</summary>
   protected IExceptionInfoHandler ExceptionInfoHandler { get; }

   /// <summary>The id of the context that this logger belongs to.</summary>
   protected ulong ContextId { get; }
   #endregion

   #region Constructors
   /// <summary>Creates an instance of a new <see cref="BaseLogger"/>.</summary>
   /// <param name="collector">The <see cref="ILogDataCollector"/> to use.</param>
   /// <param name="writeContext">The <see cref="ILogWriteContext"/> to use.</param>
   /// <param name="exceptionInfoHandler">The <see cref="IExceptionInfoHandler"/> to use.</param>
   /// <param name="contextId">The id of the context that this logger belongs to.</param>
   /// <param name="scope">The scope (inside the given <paramref name="contextId"/> that this logger belongs to.</param>
   public BaseLogger(
      ILogDataCollector collector,
      ILogWriteContext writeContext,
      IExceptionInfoHandler exceptionInfoHandler,
      ulong contextId,
      ulong scope)
   {
      Collector = collector;
      WriteContext = writeContext;
      ExceptionInfoHandler = exceptionInfoHandler;

      ContextId = contextId;
      _scope = scope;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ILogger Log(ImportanceCombination importance, string message, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = WriteContext.NewEntryId();
      TimeSpan timestamp = WriteContext.GetTimestamp();
      ulong fileId = GetFileId(file);

      MessageComponent component = new MessageComponent(message);

      Save(entryId, importance.Normalised(), timestamp, fileId, line, component);
      return this;
   }

   /// <inheritdoc/>
   public ILogger LogTag(ImportanceCombination importance, string tag, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = WriteContext.NewEntryId();
      TimeSpan timestamp = WriteContext.GetTimestamp();
      ulong fileId = GetFileId(file);
      ulong tagId = GetTagId(tag);

      TagComponent component = new TagComponent(tagId);

      Save(entryId, importance.Normalised(), timestamp, fileId, line, component);
      return this;
   }

   /// <inheritdoc/>
   public ILogger Log(ImportanceCombination importance, Thread thread, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = WriteContext.NewEntryId();
      TimeSpan timestamp = WriteContext.GetTimestamp();
      ulong fileId = GetFileId(file);

      ThreadComponent component = ThreadComponent.FromThread(thread);

      Save(entryId, importance.Normalised(), timestamp, fileId, line, component);
      return this;
   }

   /// <inheritdoc/>
   public ILogger Log(ImportanceCombination importance, Assembly assembly, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = WriteContext.NewEntryId();
      TimeSpan timestamp = WriteContext.GetTimestamp();
      ulong fileId = GetFileId(file);

      ulong assemblyId = GetAssemblyId(assembly);
      AssemblyComponent component = new AssemblyComponent(assemblyId);

      Save(entryId, importance.Normalised(), timestamp, fileId, line, component);
      return this;
   }

   /// <inheritdoc/>
   public ILogger Log(ImportanceCombination importance, StackTrace stackTrace, int? threadId, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = WriteContext.NewEntryId();
      TimeSpan timestamp = WriteContext.GetTimestamp();
      ulong fileId = GetFileId(file);

      threadId ??= -1;

      IStackTraceInfo stackTraceInfo = StackTraceInfoHelper.GetStackTraceInfo(WriteContext, Collector, stackTrace, threadId.Value);
      StackTraceComponent component = new StackTraceComponent(stackTraceInfo);

      Save(entryId, importance.Normalised(), timestamp, fileId, line, component);
      return this;
   }

   /// <inheritdoc/>
   public ILogger Log(ImportanceCombination importance, Type type, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = WriteContext.NewEntryId();
      TimeSpan timestamp = WriteContext.GetTimestamp();
      ulong fileId = GetFileId(file);

      ulong typeId = TypeInfoHelper.EnsureIdsForAssociatedTypes(WriteContext, Collector, type);
      TypeComponent component = new TypeComponent(typeId);

      Save(entryId, importance.Normalised(), timestamp, fileId, line, component);
      return this;
   }

   /// <inheritdoc/>
   public ILogger Log(ImportanceCombination importance, Exception exception, int? threadId, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = WriteContext.NewEntryId();
      TimeSpan timestamp = WriteContext.GetTimestamp();
      ulong fileId = GetFileId(file);

      IExceptionInfo exceptionInfo = ExceptionInfoHandler.Convert(exception, threadId);

      ExceptionComponent component = new ExceptionComponent(exceptionInfo);

      Save(entryId, importance.Normalised(), timestamp, fileId, line, component);
      return this;
   }

   /// <inheritdoc/>
   public IEntryBuilder StartEntry(ImportanceCombination importance, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      ulong id = WriteContext.NewEntryId();
      entryId = id;

      TimeSpan timestamp = WriteContext.GetTimestamp();
      ulong fileId = GetFileId(file);

      void SaveEntry(IReadOnlyDictionary<ComponentKind, IComponent> components)
      {
         Entry entry = new Entry(id, ContextId, _scope, importance.Normalised(), timestamp, fileId, line, components);

         Collector.Deposit(entry);
      }

      EntryBuilder builder = new EntryBuilder(this, Collector, WriteContext, SaveEntry);

      return builder;
   }

   /// <inheritdoc/>
   public ITableComponentBuilder<ILogger> StartTable(ImportanceCombination importance, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      ulong id = WriteContext.NewEntryId();
      entryId = id;

      TimeSpan timestamp = WriteContext.GetTimestamp();
      ulong fileId = GetFileId(file);

      void SaveTable(ITableComponent component)
      {
         Save(id, importance.Normalised(), timestamp, fileId, line, component);
      }

      TableComponentBuilder<ILogger> builder = new TableComponentBuilder<ILogger>(this, WriteContext, Collector, SaveTable);
      return builder;
   }
   #endregion

   #region Helpers
   /// <summary>
   /// Gets the id for the given <paramref name="file"/>, if a new id had to be created
   /// a <see cref="FileReference"/> will be deposited in the <see cref="Collector"/>.
   /// </summary>
   /// <param name="file">The file to get the id of.</param>
   /// <returns>The id of the given <paramref name="file"/>.</returns>
   protected ulong GetFileId(string file)
   {
      if (WriteContext.GetOrCreateFileId(file, out ulong fileId))
      {
         FileReference reference = new FileReference(file, fileId);
         Collector.Deposit(reference);
      }

      return fileId;
   }

   /// <summary>
   /// Gets the id for the given <paramref name="tag"/>, if a new id had to be created
   /// a <see cref="TagReference"/> will be deposited in the <see cref="Collector"/>.
   /// </summary>
   /// <param name="tag">The tag to get the id of.</param>
   /// <returns>The id of the given <paramref name="tag"/>.</returns>
   protected ulong GetTagId(string tag)
   {
      if (WriteContext.GetOrCreateFileId(tag, out ulong tagId))
      {
         TagReference reference = new TagReference(tag, tagId);
         Collector.Deposit(reference);
      }

      return tagId;
   }

   /// <summary>
   /// Gets the id for the given <paramref name="assembly"/>, if a new id had to be created
   /// an <see cref="IAssemblyInfo"/> will be deposited in the <see cref="Collector"/>.
   /// </summary>
   /// <param name="assembly">The assembly to get the id of.</param>
   /// <returns>The id of the given <paramref name="assembly"/>.</returns>
   protected ulong GetAssemblyId(Assembly assembly)
   {
      AssemblyIdentity identity = new AssemblyIdentity(assembly);
      if (WriteContext.GetOrCreateAssemblyId(identity, out ulong assemblyId))
      {
         AssemblyInfo assemblyInfo = AssemblyInfo.FromAssembly(assembly);
         AssemblyReference assemblyReference = new AssemblyReference(assemblyInfo, assemblyId);

         Collector.Deposit(assemblyReference);
      }

      return assemblyId;
   }

   private void Save(ulong entryId, ImportanceCombination importance, TimeSpan timestamp, ulong fileId, uint line, IComponent component)
   {
      Dictionary<ComponentKind, IComponent> componentsByKind = new Dictionary<ComponentKind, IComponent>
      {
         { component.Kind, component }
      };

      Entry entry = new Entry(entryId, ContextId, _scope, importance, timestamp, fileId, line, componentsByKind);
      Collector.Deposit(entry);
   }
   #endregion
}