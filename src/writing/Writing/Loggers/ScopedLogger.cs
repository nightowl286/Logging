using System.Runtime.CompilerServices;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Loggers;

namespace TNO.Logging.Writing.Loggers;

/// <summary>
/// Represents a scoped logger.
/// </summary>
public class ScopedLogger : ILogger
{
   #region Fields
   private readonly ILogDataCollector _collector;
   private readonly ILogWriteContext _writeContext;
   private readonly ulong _contextId;
   private readonly ulong _scope;
   #endregion

   #region Constructors
   /// <summary>Creates an instance of a new <see cref="ScopedLogger"/>.</summary>
   /// <param name="collector">The collector to use.</param>
   /// <param name="writeContext">The write context to use.</param>
   /// <param name="contextId">The id of the context that this logger belongs to.</param>
   /// <param name="scope">The scope (inside the given <paramref name="contextId"/> that this logger belongs to.</param>
   public ScopedLogger(ILogDataCollector collector, ILogWriteContext writeContext, ulong contextId, ulong scope)
   {
      _collector = collector;
      _writeContext = writeContext;

      _contextId = contextId;
      _scope = scope;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ILogger Log(Importance Importance, string message,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      ulong id = _writeContext.NewEntryId();
      TimeSpan timestamp = _writeContext.GetTimestamp();
      ulong fileId = GetFileId(file);

      MessageComponent component = new MessageComponent(message);

      Save(id, Importance.Normalised(), timestamp, fileId, line, component);
      return this;
   }
   #endregion

   #region Helpers
   private ulong GetFileId(string file)
   {
      if (_writeContext.GetOrCreateFileId(file, out ulong fileId))
      {
         FileReference reference = new FileReference(file, fileId);
         _collector.Deposit(reference);
      }

      return fileId;
   }
   private void Save(ulong entryId, Importance Importance, TimeSpan timestamp, ulong fileId, uint line, IComponent component)
   {
      Dictionary<ComponentKind, IComponent> componentsByKind = new Dictionary<ComponentKind, IComponent>
      {
         { component.Kind, component }
      };

      Entry entry = new Entry(entryId, _contextId, _scope, Importance, timestamp, fileId, line, componentsByKind);
      _collector.Deposit(entry);
   }
   #endregion
}