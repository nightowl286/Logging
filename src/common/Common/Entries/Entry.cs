using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Entries.Importance;

namespace TNO.Logging.Common.Entries;

/// <summary>
/// Represents a log entry.
/// </summary>
public class Entry : IEntry
{
   #region Properties
   /// <inheritdoc/>
   public ulong Id { get; }

   /// <inheritdoc/>
   public ulong ContextId { get; }

   /// <inheritdoc/>
   public ulong Scope { get; }

   /// <inheritdoc/>
   public ImportanceCombination Importance { get; }

   /// <inheritdoc/>
   public TimeSpan Timestamp { get; }

   /// <inheritdoc/>
   public ulong FileId { get; }

   /// <inheritdoc/>
   public uint LineInFile { get; }

   /// <inheritdoc/>
   public IReadOnlyDictionary<ComponentKind, IComponent> Components { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="Entry"/>.</summary>
   /// <param name="id">The id of this entry.</param>
   /// <param name="contextId">The id of the context that this entry belongs to.</param>
   /// <param name="scope">The scope (in the current context) that this entry belongs to.</param>
   /// <param name="importance">The severity, and purpose, of this entry.</param>
   /// <param name="timestamp">The timestamp of when this entry was created (since the log was created).</param>
   /// <param name="fileId">The id of the file where this entry has been logged.</param>
   /// <param name="lineInFile">
   /// The line number in the file (specified by the <paramref name="fileId"/>)
   /// where this entry has been logged.
   /// </param>
   /// <param name="components">The components that this entry contains.</param>
   public Entry(
      ulong id,
      ulong contextId,
      ulong scope,
      ImportanceCombination importance,
      TimeSpan timestamp,
      ulong fileId,
      uint lineInFile,
      IReadOnlyDictionary<ComponentKind, IComponent> components)
   {
      Id = id;
      ContextId = contextId;
      Scope = scope;
      Importance = importance;
      Timestamp = timestamp;
      FileId = fileId;
      LineInFile = lineInFile;
      Components = components;
   }
   #endregion
}