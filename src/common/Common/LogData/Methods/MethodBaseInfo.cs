using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Methods;

namespace TNO.Logging.Common.LogData.Methods;

/// <summary>
/// Represents info about a <see cref="MethodBase"/>.
/// </summary>
/// <param name="DeclaringTypeId">The id of the <see cref="MemberInfo.DeclaringType"/>.</param>
/// <param name="ParameterInfos">The list of infos about the <see cref="MethodBase.GetParameters"/>.</param>
/// <param name="Name">The name of the method.</param>
public abstract record class MethodBaseInfo(
   ulong DeclaringTypeId,
   IReadOnlyList<IParameterInfo> ParameterInfos,
   string Name) : IMethodBaseInfo;
