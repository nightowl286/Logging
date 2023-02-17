using System.Runtime.CompilerServices;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Loggers;

namespace TNO.Logging.Writing.Loggers;

/// <summary>
/// Represents a scoped logger.
/// </summary>
public class ScopedLogger : ILogger
{
   #region Fields
   private readonly ILogDataCollector _collector;
   private readonly ILogWriteContext _context;
   #endregion

   #region Constructors
   /// <summary>Creates an instance of a new <see cref="ScopedLogger"/>.</summary>
   /// <param name="collector">The collector to use.</param>
   /// <param name="context">The context to use.</param>
   public ScopedLogger(ILogDataCollector collector, ILogWriteContext context)
   {
      _collector = collector;
      _context = context;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ILogger Log(Importance Importance, string message,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      ulong id = _context.NewEntryId();
      TimeSpan timestamp = _context.GetTimestamp();
      ulong fileId = GetFileId(file);

      MessageComponent component = new MessageComponent(message);

      Save(id, Importance.Normalised(), timestamp, fileId, line, component);
      return this;
   }
   #endregion

   #region Helpers
   private ulong GetFileId(string file)
   {
      if (_context.GetOrCreateFileId(file, out ulong fileId))
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

      Entry entry = new Entry(entryId, Importance, timestamp, fileId, line, componentsByKind);
      _collector.Deposit(entry);
   }
   #endregion
}