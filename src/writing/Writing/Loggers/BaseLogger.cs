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

   /// <summary>The id of the context that this logger belongs to.</summary>
   protected ulong ContextId { get; }
   #endregion

   #region Constructors
   /// <summary>Creates an instance of a new <see cref="BaseLogger"/>.</summary>
   /// <param name="collector">The collector to use.</param>
   /// <param name="writeContext">The write context to use.</param>
   /// <param name="contextId">The id of the context that this logger belongs to.</param>
   /// <param name="scope">The scope (inside the given <paramref name="contextId"/> that this logger belongs to.</param>
   public BaseLogger(ILogDataCollector collector, ILogWriteContext writeContext, ulong contextId, ulong scope)
   {
      Collector = collector;
      WriteContext = writeContext;

      ContextId = contextId;
      _scope = scope;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ILogger Log(Importance Importance, string message, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = WriteContext.NewEntryId();
      TimeSpan timestamp = WriteContext.GetTimestamp();
      ulong fileId = GetFileId(file);

      MessageComponent component = new MessageComponent(message);

      Save(entryId, Importance.Normalised(), timestamp, fileId, line, component);
      return this;
   }
   #endregion

   #region Helpers
   /// <summary>
   /// Gets the id for the given <paramref name="file"/>, if a new id had to be created
   /// a <see cref="FileReference"/> will be deposited in the <see cref="Collector"/>.
   /// </summary>
   /// <param name="file">The file to get the id of.</param>
   /// <returns>The id of the given <paramref name="file"/></returns>
   protected ulong GetFileId(string file)
   {
      if (WriteContext.GetOrCreateFileId(file, out ulong fileId))
      {
         FileReference reference = new FileReference(file, fileId);
         Collector.Deposit(reference);
      }

      return fileId;
   }
   private void Save(ulong entryId, Importance Importance, TimeSpan timestamp, ulong fileId, uint line, IComponent component)
   {
      Dictionary<ComponentKind, IComponent> componentsByKind = new Dictionary<ComponentKind, IComponent>
      {
         { component.Kind, component }
      };

      Entry entry = new Entry(entryId, ContextId, _scope, Importance, timestamp, fileId, line, componentsByKind);
      Collector.Deposit(entry);
   }
   #endregion
}