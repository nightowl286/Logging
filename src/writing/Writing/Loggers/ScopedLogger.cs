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
   public ILogger Log(SeverityAndPurpose severityAndPurpose, string message)
   {
      ulong id = _context.NewEntryId();
      TimeSpan timestamp = _context.GetTimestamp();
      MessageComponent component = new MessageComponent(message);

      Save(id, severityAndPurpose, timestamp, component);
      return this;
   }
   #endregion

   #region Helpers
   private void Save(ulong entryId, SeverityAndPurpose severityAndPurpose, TimeSpan timestamp, IComponent component)
   {
      Dictionary<ComponentKind, IComponent> componentsByKind = new Dictionary<ComponentKind, IComponent>
      {
         { component.Kind, component }
      };

      Entry entry = new Entry(entryId, severityAndPurpose, timestamp, componentsByKind);
      _writer.RequestWrite(entry);
   }
   #endregion
}
