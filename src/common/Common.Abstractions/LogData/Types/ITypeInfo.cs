using System;
using System.Collections.Generic;
using System.Reflection;

namespace TNO.Logging.Common.Abstractions.LogData.Types;

/// <summary>
/// Denotes info about a <see cref="Type"/>.
/// </summary>
public interface ITypeInfo
{
   #region Properties
   /// <summary>The id of the assembly the type is defined in.</summary>
   /// <remarks>This id is only used within the log.</remarks>
   ulong AssemblyId { get; }

   /// <summary>The id of the <see cref="Type.DeclaringType"/>.</summary>
   /// <remarks>
   /// An id of <c>0</c> means the type is not declared in a different type.
   /// This id is only used within the log.
   /// </remarks>
   ulong DeclaringTypeId { get; }

   /// <summary>The id of the <see cref="Type.BaseType"/>.</summary>
   /// <remarks>
   /// An id of <c>0</c> means no base type.
   /// This id is only used within the log.
   /// </remarks>
   ulong BaseTypeId { get; }

   /// <summary>The id of the <see cref="Type.GetElementType"/>.</summary>
   /// <remarks>
   /// An id of <c>0</c> means no element type.
   /// This id is only used within the log.
   /// </remarks>
   ulong ElementTypeId { get; }

   /// <summary>The id of the <see cref="Type.GetGenericTypeDefinition"/></summary>
   /// <remarks>
   /// An id of <c>0</c> means no generic type definition.
   /// This id is only used within the log.
   /// </remarks>
   ulong GenericTypeDefinitionId { get; }

   /// <summary>The <see cref="MemberInfo.Name"/> of the <see cref="Type"/>.</summary>
   string Name { get; }

   /// <summary>The <see cref="Type.FullName"/>.</summary>
   /// <remarks>
   /// This value can be equivalent to <see cref="string.Empty"/> 
   /// (instead of <see langword="null"/> as the documentation says) under different conditions,
   /// as described in the <see href="https://learn.microsoft.com/en-us/dotnet/api/system.type.fullname#property-value">documentation</see>.
   /// </remarks>
   string FullName { get; }

   /// <summary>The <see cref="Type.Namespace"/>.</summary>
   /// <remarks>
   /// This value can be equivalent to <see cref="string.Empty"/> 
   /// (instead of <see langword="null"/> as the documentation says) under different conditions,
   /// as described in the <see href="https://learn.microsoft.com/en-us/dotnet/api/system.type.namespace#property-value">documentation</see>.
   /// </remarks>
   string Namespace { get; }

   /// <summary>The ids of the <see cref="Type.GenericTypeArguments"/>.</summary>
   IReadOnlyList<ulong> GenericTypeIds { get; }
   #endregion
}
