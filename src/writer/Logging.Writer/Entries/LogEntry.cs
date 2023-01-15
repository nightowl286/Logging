using TNO.Logging.Writer.Abstractions;
using TNO.Logging.Writer.Entries.Components;

namespace TNO.Logging.Writer.Entries;

internal record class LogEntry(
   ulong Context,
   ulong FileRef,
   int Line,
   ulong Id,
   Severity Severity,
   IReadOnlyCollection<ComponentBase> Components);
