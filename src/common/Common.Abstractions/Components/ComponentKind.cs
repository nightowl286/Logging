using System;

namespace TNO.Common.Abstractions.Components;

/// <summary>
/// A flags enum that represents the allowed components an entry can have.
/// </summary>
[Flags]
public enum ComponentKind : ushort
{
   /// <summary>A <see cref="string"/> message component.</summary>
   Message = 1,

   /// <summary>A <see cref="System.Diagnostics.StackFrame"/> component.</summary>
   StackFrame = 2,

   /// <summary>A <see cref="System.Diagnostics.StackTrace"/> component.</summary>
   StackTrace = 4,

   /// <summary>An <see cref="System.Exception"/> component.</summary>
   Exception = 8,

   /// <summary>A <see cref="System.Threading.Thread"/> component.</summary>
   Thread = 16,

   /// <summary>An <see cref="System.Reflection.Assembly"/> component.</summary>
   Assembly = 32,

   /// <summary>An entry reference component.</summary>
   EntryLink = 64,

   /// <summary>A tag reference component.</summary>
   Tag = 128,

   /// <summary>An additional file path component.</summary>
   AdditionalFile = 256,

   /// <summary>
   /// A value that will be used to indicate that the size 
   /// of the <see cref="ComponentKind"/> enum has grown.
   /// </summary>
   ReservedForExpansion = 32_768,
}
