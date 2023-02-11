using System.Runtime.CompilerServices;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Abstractions.Loggers;

namespace TNO.Logging.Writing.Loggers;

/// <summary>
/// Represents a scoped logger.
/// </summary>
public class ScopedLogger : ILogger
{
   #region Fields
   private readonly ILogWriter _writer;
   private readonly ILogWriteContext _context;
   #endregion

   #region Constructors
   /// <summary>Creates an instance of a new <see cref="ScopedLogger"/>.</summary>
   /// <param name="writer">The writer to use.</param>
   /// <param name="context">The context to use.</param>
   public ScopedLogger(ILogWriter writer, ILogWriteContext context)
   {
      _writer = writer;
      _context = context;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ILogger Log(SeverityAndPurpose severityAndPurpose, string message,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      ulong id = _context.NewEntryId();
      TimeSpan timestamp = _context.GetTimestamp();
      ulong fileId = GetFileId(file);

      MessageComponent component = new MessageComponent(message);

      Save(id, severityAndPurpose.Normalised(), timestamp, fileId, line, component);
      return this;
   }
   #endregion

   #region Helpers
   private ulong GetFileId(string file)
   {
      if (_context.GetOrCreateFileId(file, out ulong fileId))
      {
         FileReference reference = new FileReference(file, fileId);
         _writer.RequestWrite(reference);
      }

      return fileId;
   }
   private void Save(ulong entryId, SeverityAndPurpose severityAndPurpose, TimeSpan timestamp, ulong fileId, uint line, IComponent component)
   {
      Dictionary<ComponentKind, IComponent> componentsByKind = new Dictionary<ComponentKind, IComponent>
      {
         { component.Kind, component }
      };

      Entry entry = new Entry(entryId, severityAndPurpose, timestamp, fileId, line, componentsByKind);
      _writer.RequestWrite(entry);
   }
   #endregion
}
