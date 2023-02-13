using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Abstractions.DataKinds;

/// <summary>
/// Represents the different kinds of versioned data.
/// </summary>
public enum VersionedDataKind : ushort
{
   /// <summary>Represents the <see cref="IEntry"/>.</summary>
   Entry,

   /// <summary>Represents the <see cref="IMessageComponent"/>.</summary>
   Message,

   /// <summary>Represents the <see cref="Abstractions.FileReference"/>.</summary>
   FileReference,
}