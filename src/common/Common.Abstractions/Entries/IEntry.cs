﻿using System;
using System.Collections.Generic;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// Denotes a log entry.
/// </summary>
public interface IEntry
{
   #region Properties
   /// <summary>The id of this entry.</summary>
   ulong Id { get; }

   /// <summary>The timestamp of when this entry was created (since the log was created).</summary>
   TimeSpan Timestamp { get; }

   /// <summary>The components that this entry contains.</summary>
   IReadOnlyDictionary<ComponentKind, IComponent> Components { get; }
   #endregion
}
