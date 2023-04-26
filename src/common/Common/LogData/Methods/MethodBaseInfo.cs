using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Methods;

namespace TNO.Logging.Common.LogData.Methods;

/// <summary>
/// Represents info about a <see cref="MethodBase"/>.
/// </summary>
public abstract class MethodBaseInfo : IMethodBaseInfo
{
   #region Properties
   /// <inheritdoc/>
   public ulong DeclaringTypeId { get; }

   /// <inheritdoc/>
   public IReadOnlyList<IParameterInfo> ParameterInfos { get; }

   /// <inheritdoc/>
   public string Name { get; }
   #endregion

   #region Constructors
   /// <summary>Populates the properties of the <see cref="MethodBaseInfo"/>.</summary>
   /// <param name="declaringTypeId">The id of the <see cref="MemberInfo.DeclaringType"/>.</param>
   /// <param name="parameterInfos">The list of infos about the <see cref="MethodBase.GetParameters"/>.</param>
   /// <param name="name">The name of the method.</param>
   public MethodBaseInfo(ulong declaringTypeId,
   IReadOnlyList<IParameterInfo> parameterInfos,
   string name)
   {
      DeclaringTypeId = declaringTypeId;
      ParameterInfos = parameterInfos;
      Name = name;
   }
   #endregion
}
