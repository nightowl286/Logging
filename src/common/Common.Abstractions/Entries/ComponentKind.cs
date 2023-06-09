﻿using System;

namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// Represents the kind of a component.
/// </summary>
[Flags]
public enum ComponentKind : ushort
{
   /// <summary>A simple <see cref="string"/> message.</summary>
   Message = 1,

   /// <summary>A tag that should be used for programmatically finding relevant entries.</summary>
   Tag = 2,

   /// <summary>Information gathered about a <see cref="System.Threading.Thread"/>.</summary>
   Thread = 4,

   /// <summary>A link to a different <see cref="IEntry"/>.</summary>
   EntryLink = 8,

   /// <summary>A table that can contain custom key/value pairs.</summary>
   Table = 16,

   /// <summary>Information gathered about a <see cref="System.Reflection.Assembly"/>.</summary>
   Assembly = 32,

   /// <summary>Information gathered about a <see cref="System.Type"/>.</summary>
   Type = 64,

   /// <summary>Information about a <see cref="StackTrace"/>.</summary>
   StackTrace = 128,

   /// <summary>Information about an <see cref="System.Exception"/>.</summary>
   Exception = 256,

   /// <summary>This entry is reserved for possible future expansion.</summary>
   ReservedForExpansion = 32_768,
}