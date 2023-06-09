﻿using System;
using System.Collections.Generic;

using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Entries.Importance;

namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// Denotes a log entry.
/// </summary>
public interface IEntry
{
   #region Properties
   /// <summary>The id of this entry.</summary>
   ulong Id { get; }

   /// <summary>The severity, and purpose, of this entry.</summary>
   ImportanceCombination Importance { get; }

   /// <summary>The timestamp of when this entry was created (since the log was created).</summary>
   TimeSpan Timestamp { get; }

   /// <summary>The id of the file where this entry has been logged.</summary>
   ulong FileId { get; }

   /// <summary>
   /// The line number in the file (specified by the <see cref="FileId"/>)
   /// where this entry has been logged.
   /// </summary>
   uint LineInFile { get; }

   /// <summary>The id of the context that this entry belongs to.</summary>
   ulong ContextId { get; }

   /// <summary>The scope (in the current context) that this entry belongs to.</summary>
   ulong Scope { get; }

   /// <summary>The components that this entry contains.</summary>
   IReadOnlyDictionary<ComponentKind, IComponent> Components { get; }
   #endregion
}