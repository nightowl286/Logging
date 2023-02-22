using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Entries;
using TNO.Logging.Common.Entries.Components;
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

   private readonly ulong _entryId;
   private readonly ulong _contextId;
   private readonly ulong _scope;
   private readonly Importance _importance;
   private readonly TimeSpan _timestamp;
   private readonly ulong _fileId;
   private readonly uint _lineInFile;
   private readonly Dictionary<ComponentKind, IComponent> _components = new Dictionary<ComponentKind, IComponent>();
   #endregion

   #region Constructors
   public EntryBuilder(
      ILogger logger,
      ILogDataCollector collector,
      ILogWriteContext writeContext,
      ulong entryId,
      ulong contextId,
      ulong scope,
      Importance importance,
      TimeSpan timestamp,
      ulong fileId,
      uint lineInFile)
   {
      _logger = logger;
      _collector = collector;
      _writeContext = writeContext;

      _entryId = entryId;
      _contextId = contextId;
      _scope = scope;
      _importance = importance;
      _timestamp = timestamp;
      _fileId = fileId;
      _lineInFile = lineInFile;
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
   public ILogger FinishEntry()
   {
      Entry entry = new Entry(
         _entryId,
         _contextId,
         _scope,
         _importance,
         _timestamp,
         _fileId,
         _lineInFile,
         _components);

      _collector.Deposit(entry);

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
