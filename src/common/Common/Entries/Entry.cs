using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Entries;

/// <summary>
/// Represents a log entry.
/// </summary>
/// <param name="Id">The id of this entry.</param>
/// <param name="SeverityAndPurpose">The severity, and purpose, of this entry.</param>
/// <param name="Timestamp">The timestamp of when this entry was created (since the log was created).</param>
/// <param name="Components">The components that this entry contains.</param>
public record class Entry(
   ulong Id,
   SeverityAndPurpose SeverityAndPurpose,
   TimeSpan Timestamp,
   IReadOnlyDictionary<ComponentKind, IComponent> Components) : IEntry;
