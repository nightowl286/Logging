using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using ReflectionMethodInfo = System.Reflection.MethodInfo;

namespace TNO.Logging.Common.LogData.Methods;

/// <summary>
/// Represents info about a <see cref="ReflectionMethodInfo"/>.
/// </summary>
public class MethodInfo : MethodBaseInfo, IMethodInfo
{
   #region Properties
   /// <inheritdoc/>
   public ulong ReturnTypeId { get; }

   /// <inheritdoc/>
   public IReadOnlyList<ulong> GenericTypeIds { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="MethodInfo"/>.</summary>
   /// <param name="declaringTypeId">The id of the <see cref="MemberInfo.DeclaringType"/>.</param>
   /// <param name="parameterInfos">The list of infos about the <see cref="MethodBase.GetParameters"/>.</param>
   /// <param name="name">The name of the method.</param>
   /// <param name="returnTypeId">The id of the <see cref="ReflectionMethodInfo.ReturnType"/>.</param>
   /// <param name="genericTypeIds">The ids of the <see cref="ReflectionMethodInfo.GetGenericArguments"/>.</param>
   public MethodInfo(
      ulong declaringTypeId,
      IReadOnlyList<IParameterInfo> parameterInfos,
      string name,
      ulong returnTypeId,
      IReadOnlyList<ulong> genericTypeIds)
      : base(declaringTypeId, parameterInfos, name)
   {
      ReturnTypeId = returnTypeId;
      GenericTypeIds = genericTypeIds;
   }
   #endregion

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
