using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.LogData;

namespace TNO.Logging.Reading.LogData.TypeInfos;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="ITypeInfo"/>.
/// </summary>
internal static class TypeInfoFactory
{
   #region Functions
   public static ITypeInfo Version0(
      ulong assemblyId,
      ulong baseTypeId,
      ulong declaringTypeId,
      ulong elementTypeId,
      ulong genericTypeDefinitionId,
      string name,
      string fullName,
      string @namespace,
      IReadOnlyList<ulong> genericTypeIds)
   {
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
