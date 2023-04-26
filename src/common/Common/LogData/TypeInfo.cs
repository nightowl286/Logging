using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Types;

namespace TNO.Logging.Common.LogData;

/// <summary>
/// Represents info about a <see cref="Type"/>.
/// </summary>
public class TypeInfo : ITypeInfo
{
   #region Properties
   /// <inheritdoc/>
   public ulong AssemblyId { get; }

   /// <inheritdoc/>
   public ulong DeclaringTypeId { get; }

   /// <inheritdoc/>
   public ulong BaseTypeId { get; }

   /// <inheritdoc/>
   public ulong ElementTypeId { get; }

   /// <inheritdoc/>
   public ulong GenericTypeDefinitionId { get; }

   /// <inheritdoc/>
   public string Name { get; }

   /// <inheritdoc/>
   public string FullName { get; }

   /// <inheritdoc/>
   public string Namespace { get; }

   /// <inheritdoc/>
   public IReadOnlyList<ulong> GenericTypeIds { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TypeInfo"/>.</summary>
   /// <param name="assemblyId">The id of the assembly the type is defined in.</param>
   /// <param name="baseTypeId">The id of the <see cref="Type.BaseType"/>.</param>
   /// <param name="declaringTypeId">The id of the <see cref="Type.DeclaringType"/>.</param>
   /// <param name="elementTypeId">The id of the <see cref="Type.GetElementType"/>.</param>
   /// <param name="genericTypeDefinitionId">The id of the <see cref="Type.GetGenericTypeDefinition"/>.</param>
   /// <param name="name">The <see cref="MemberInfo.Name"/> of the <see cref="Type"/>.</param>
   /// <param name="fullName">The <see cref="Type.FullName"/>, or <see cref="string.Empty"/> instead of <see langword="null"/>.</param>
   /// <param name="namespace">The <see cref="Type.Namespace"/>, or <see cref="string.Empty"/> instead of <see langword="null"/>.</param>
   /// <param name="genericTypeIds">The ids of the <see cref="Type.GenericTypeArguments"/>.</param>
   public TypeInfo(
      ulong assemblyId,
      ulong declaringTypeId,
      ulong baseTypeId,
      ulong elementTypeId,
      ulong genericTypeDefinitionId,
      string name,
      string fullName,
      string @namespace,
      IReadOnlyList<ulong> genericTypeIds)
   {
      AssemblyId = assemblyId;
      DeclaringTypeId = declaringTypeId;
      BaseTypeId = baseTypeId;
      ElementTypeId = elementTypeId;
      GenericTypeDefinitionId = genericTypeDefinitionId;
      Name = name;
      FullName = fullName;
      Namespace = @namespace;
      GenericTypeIds = genericTypeIds;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public override string? ToString() => $"TypeInfo: {FullName}";
   #endregion

   #region Functions
   /// <summary>
   /// Creates a <see cref="TypeInfo"/> for the given <paramref name="type"/>.
   /// </summary>
   /// <param name="assemblyId">The id that will be assigned to the created <see cref="AssemblyId"/>.</param>
   /// <param name="declaringTypeId">The id that will be assigned to the created <see cref="DeclaringTypeId"/>.</param>
   /// <param name="baseTypeId">The id that will be assigned to the created <see cref="BaseTypeId"/>.</param>
   /// <param name="elementTypeId">The id that will be assigned to the created <see cref="ElementTypeId"/>.</param>
   /// <param name="genericTypeDefinitionId">The id that will be assigned to the created <see cref="GenericTypeDefinitionId"/>.</param>
   /// <param name="genericTypeIds">The ids that will be assigned to the created <see cref="GenericTypeIds"/>.</param>
   /// <param name="type">The type to create the <see cref="TypeInfo"/> for.</param>
   /// <returns>The created <see cref="TypeInfo"/>.</returns>
   public static TypeInfo FromType(
      ulong assemblyId,
      ulong declaringTypeId,
      ulong baseTypeId,
      ulong elementTypeId,
      ulong genericTypeDefinitionId,
      IReadOnlyList<ulong> genericTypeIds, Type type)
   {
      string name = type.Name;
      string fullName = type.FullName ?? string.Empty;
      string @namespace = type.Namespace ?? string.Empty;

      return new TypeInfo(
         assemblyId,
         declaringTypeId,
         baseTypeId,
         elementTypeId,
         genericTypeDefinitionId,
         name,
         fullName,
         @namespace,
         genericTypeIds);
   }
   #endregion
}
