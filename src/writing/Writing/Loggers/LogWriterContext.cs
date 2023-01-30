using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.Loggers.IdFactories;

namespace TNO.Logging.Writing.Loggers;

/// <summary>
/// Represents a context that belongs to an <see cref="ILogWriter"/>.
/// </summary>
public class LogWriterContext : ILogWriteContext
{
   #region Fields
   private readonly SafeIdFactory _entryIdFactory = new SafeIdFactory(1);
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ulong NewEntryId() => _entryIdFactory.GetNext();
   #endregion
}
