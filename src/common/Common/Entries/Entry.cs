using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Entries;

/// <summary>
/// Represents a log entry.
/// </summary>
/// <param name="Id">The id of this entry.</param>
/// <param name="Importance">The severity, and purpose, of this entry.</param>
/// <param name="Timestamp">The timestamp of when this entry was created (since the log was created).</param>
/// <param name="FileId">The id of the file where this entry has been logged.</param>
/// <param name="LineInFile">
/// The line number in the file (specified by the <see cref="FileId"/>)
/// where this entry has been logged.
/// </param>
/// <param name="Components">The components that this entry contains.</param>
public record class Entry(
   ulong Id,
   Importance Importance,
   TimeSpan Timestamp,
   ulong FileId,
   uint LineInFile,
   IReadOnlyDictionary<ComponentKind, IComponent> Components) : IEntry;