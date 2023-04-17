using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.LogData.Tables;
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

   /// <summary>Represents the <see cref="LogData.Tables.TableKeyReference"/>.</summary>
   TableKeyReference,

   /// <summary>Represents the <see cref="IAssemblyInfo"/>.</summary>
   AssemblyInfo,

   /// <summary>Represents the <see cref="LogData.Assemblies.AssemblyReference"/>.</summary>
   AssemblyReference,

   /// <summary>Represents the <see cref="ITypeInfo"/>.</summary>
   TypeInfo,

   /// <summary>Represents the <see cref="LogData.Types.TypeReference"/>.</summary>
   TypeReference,

   /// <summary>Represents the <see cref="IParameterInfo"/>.</summary>
   ParameterInfo,

   /// <summary>Represents the <see cref="IMethodInfo"/>.</summary>
   MethodInfo,

   /// <summary>Represents the <see cref="IConstructorInfo"/>.</summary>
   ConstructorInfo,

   /// <summary>Represents the <see cref="IStackFrameInfo"/>.</summary>
   StackFrameInfo,

   /// <summary>Represents the <see cref="IStackTraceInfo"/>.</summary>
   StackTraceInfo,

   /// <summary>Represents the <see cref="ITableInfo"/>.</summary>
   TableInfo,

   /// <summary>Represents the <see cref="IExceptionInfo"/>.</summary>
   ExceptionInfo,

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

   /// <summary>Represents the <see cref="IStackTraceComponent"/>.</summary>
   StackTrace,

   /// <summary>Represents the <see cref="ITypeComponent"/>.</summary>
   Type,

   /// <summary>Represents the <see cref="IExceptionComponent"/>.</summary>
   Exception,
}