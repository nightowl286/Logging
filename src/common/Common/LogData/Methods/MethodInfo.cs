using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using ReflectionMethodInfo = System.Reflection.MethodInfo;

namespace TNO.Logging.Common.LogData.Methods;

/// <summary>
/// Represents info about a <see cref="ReflectionMethodInfo"/>.
/// </summary>
/// <param name="DeclaringTypeId">The id of the <see cref="MemberInfo.DeclaringType"/>.</param>
/// <param name="ParameterInfos">The list of infos about the <see cref="MethodBase.GetParameters"/>.</param>
/// <param name="Name">The name of the method.</param>
/// <param name="ReturnTypeId">The id of the <see cref="ReflectionMethodInfo.ReturnType"/>.</param>
/// <param name="GenericTypeIds">The ids of the <see cref="ReflectionMethodInfo.GetGenericArguments"/>.</param>
public record class MethodInfo(
   ulong DeclaringTypeId,
   IReadOnlyList<IParameterInfo> ParameterInfos,
   string Name,
   ulong ReturnTypeId,
   IReadOnlyList<ulong> GenericTypeIds) : MethodBaseInfo(DeclaringTypeId, ParameterInfos, Name), IMethodInfo
{
   #region Functions
   /// <summary>
   /// Creates a <see cref="MethodInfo"/> for the given <paramref name="reflectionMethodInfo"/>.
   /// </summary>
   /// <param name="declaringTypeId">The id that will be assigned to the created <see cref="MethodBaseInfo.DeclaringTypeId"/>.</param>
   /// <param name="parameterInfos">The parameter infos that will be assigned to the created <see cref="MethodBaseInfo.ParameterInfos"/>.</param>
   /// <param name="returnTypeId">The id that will be assigned to the created <see cref="ReturnTypeId"/>.</param>
   /// <param name="genericTypeIds">The ids that will be assigned to the created <see cref="GenericTypeIds"/>.</param>
   /// <param name="reflectionMethodInfo">The method to create the <see cref="MethodInfo"/> for.</param>
   /// <returns>The created <see cref="MethodInfo"/>.</returns>
   public static MethodInfo FromReflectionMethodInfo(
      ulong declaringTypeId,
      IReadOnlyList<IParameterInfo> parameterInfos,
      ulong returnTypeId,
      IReadOnlyList<ulong> genericTypeIds,
      ReflectionMethodInfo reflectionMethodInfo)
   {
      string name = reflectionMethodInfo.Name;

      return new MethodInfo(declaringTypeId,
         parameterInfos,
         name,
         returnTypeId,
         genericTypeIds);
   }
   #endregion
}
