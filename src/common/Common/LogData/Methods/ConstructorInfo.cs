using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using ReflectionConstructorInfo = System.Reflection.ConstructorInfo;

namespace TNO.Logging.Common.LogData.Methods;

/// <summary>
/// Represents info about a <see cref="ReflectionConstructorInfo"/>.
/// </summary>
public class ConstructorInfo : MethodBaseInfo, IConstructorInfo
{
   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ConstructorInfo"/>.</summary>
   /// <param name="declaringTypeId">The id of the <see cref="MemberInfo.DeclaringType"/>.</param>
   /// <param name="parameterInfos">The list of infos about the <see cref="MethodBase.GetParameters"/>.</param>
   /// <param name="name">The name of the method.</param>
   public ConstructorInfo(
      ulong declaringTypeId,
      IReadOnlyList<IParameterInfo> parameterInfos,
      string name) : base(declaringTypeId, parameterInfos, name)
   { }
   #endregion

   #region Functions
   /// <summary>
   /// Creates a <see cref="ConstructorInfo"/> for the given <paramref name="reflectionConstructorInfo"/>.
   /// </summary>
   /// <param name="declaringTypeId">The id that will be assigned to the created <see cref="MethodBaseInfo.DeclaringTypeId"/>.</param>
   /// <param name="parameterInfos">The parameter infos that will be assigned to the created <see cref="MethodBaseInfo.ParameterInfos"/>.</param>
   /// <param name="reflectionConstructorInfo">The constructor to create the <see cref="ConstructorInfo"/> for.</param>
   /// <returns>The created <see cref="ConstructorInfo"/>.</returns>
   public static ConstructorInfo FromReflectionConstructorInfo(
      ulong declaringTypeId,
      IReadOnlyList<IParameterInfo> parameterInfos,
      ReflectionConstructorInfo reflectionConstructorInfo)
   {
      string name = reflectionConstructorInfo.Name;

      return new ConstructorInfo(
         declaringTypeId,
         parameterInfos,
         name);
   }
   #endregion
}
