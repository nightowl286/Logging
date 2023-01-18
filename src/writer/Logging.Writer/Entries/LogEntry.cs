using TNO.Common.Abstractions;
using TNO.Common.Abstractions.Components;
using TNO.Logging.Writer.Abstractions;

namespace TNO.Logging.Writer.Entries;

internal record class LogEntry(
   ulong ContextId,
   ulong FileId,
   int Line,
   ulong Id,
   Severity Severity,
   IReadOnlyCollection<IEntryComponent> Components) : ILogEntry;
