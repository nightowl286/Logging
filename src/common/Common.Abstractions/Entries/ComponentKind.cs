using System;

namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// Represents the kind of a component.
/// </summary>
[Flags]
public enum ComponentKind : ushort
{
   /// <summary>A simple <see cref="string"/> message.</summary>
   Message = 1,

   /// <summary>This entry is reserved for possible future expansion.</summary>
   ReservedForExpansion = 32_768,
}
