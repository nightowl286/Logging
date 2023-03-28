using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Types;

namespace TNO.Logging.Common.LogData;

/// <summary>
/// Represents info about a <see cref="Type"/>.
/// </summary>
/// <param name="Id">The id of the type.</param>
/// <param name="AssemblyId">The id of the assembly the type is defined in.</param>
/// <param name="BaseTypeId">The id of the <see cref="Type.BaseType"/>.</param>
/// <param name="DeclaringTypeId">The id of the <see cref="Type.DeclaringType"/>.</param>
/// <param name="Name">The <see cref="MemberInfo.Name"/> of the <see cref="Type"/>.</param>
/// <param name="FullName">The <see cref="Type.FullName"/>, or <see cref="string.Empty"/> instead of <see langword="null"/>.</param>
/// <param name="Namespace">The <see cref="Type.Namespace"/>, or <see cref="string.Empty"/> instead of <see langword="null"/>.</param>
public record class TypeInfo(
   ulong Id,
   ulong AssemblyId,
   ulong DeclaringTypeId,
   ulong BaseTypeId,
   string Name,
   string FullName,
   string Namespace) : ITypeInfo
{
   #region Functions
   /// <summary>
   /// Creates a <see cref="TypeInfo"/> with the given 
   /// <paramref name="id"/>, for the given <paramref name="type"/>.
   /// </summary>
   /// <param name="id">The id that will be assigned to the created <see cref="TypeInfo"/>.</param>
   /// <param name="assemblyId">The id that will be assigned to the created <see cref="AssemblyId"/>.</param>
   /// <param name="declaringTypeId">The id that will be assigned to the created <see cref="DeclaringTypeId"/>.</param>
   /// <param name="baseTypeId">The id that will be assigned to the created <see cref="BaseTypeId"/>.</param>
   /// <param name="type">The type to create the <see cref="TypeInfo"/> for.</param>
   /// <returns>The created <see cref="TypeInfo"/>.</returns>
   public static TypeInfo FromType(ulong id, ulong assemblyId, ulong declaringTypeId, ulong baseTypeId, Type type)
   {
      string name = type.Name;
      string fullName = type.FullName ?? string.Empty;
      string @namespace = type.Namespace ?? string.Empty;

      return new TypeInfo(
         id,
         assemblyId,
         declaringTypeId,
         baseTypeId,
         name,
         fullName,
         @namespace);
   }
   #endregion
}
