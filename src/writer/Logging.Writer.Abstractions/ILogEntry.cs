using System.Collections.Generic;

namespace TNO.Logging.Writer.Abstractions;
public interface ILogEntry
{
   #region Properties
   ulong Id { get; }
   ulong ContextId { get; }
   ulong FileId { get; }
   int Line { get; }
   Severity Severity { get; }
   IReadOnlyCollection<IEntryComponent> Components { get; }
   #endregion
}
