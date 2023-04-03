using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Types;

namespace TNO.Logging.Common.Abstractions.DataKinds;

/// <summary>
/// Represents the different kinds of versioned data.
/// </summary>
public enum VersionedDataKind : ushort
{
   /// <summary>Represents the <see cref="IEntry"/>.</summary>
   Entry,

   /// <summary>Represents the <see cref="LogData.FileReference"/>.</summary>
   FileReference,

   /// <summary>Represents the <see cref="LogData.ContextInfo"/>.</summary>
   ContextInfo,

   /// <summary>Represents the <see cref="LogData.TagReference"/>.</summary>
   TagReference,

   /// <summary>Represents the <see cref="LogData.TableKeyReference"/>.</summary>
   TableKeyReference,

   /// <summary>Represents the <see cref="IAssemblyInfo"/>.</summary>
   AssemblyInfo,

   /// <summary>Represents the <see cref="LogData.AssemblyReference"/>.</summary>
   AssemblyReference,

   /// <summary>Represents the <see cref="ITypeInfo"/>.</summary>
   TypeInfo,

   /// <summary>Represents the <see cref="IMessageComponent"/>.</summary>
   Message,

   /// <summary>Represents the <see cref="ITagComponent"/>.</summary>
   Tag,

   /// <summary>Represents the <see cref="IThreadComponent"/>.</summary>
   Thread,

   /// <summary>Represents the <see cref="IEntryLinkComponent"/>.</summary>
   EntryLink,

   /// <summary>Represents the <see cref="ITableComponent"/>.</summary>
   Table,

   /// <summary>Represents the <see cref="IAssemblyComponent"/>.</summary>
   Assembly,

   /// <summary>Represents the <see cref="ISimpleStackTraceComponent"/>.</summary>
   SimpleStackTrace,
}