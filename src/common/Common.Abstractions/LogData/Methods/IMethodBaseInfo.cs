using System.Collections.Generic;
using System.Reflection;

namespace TNO.Logging.Common.Abstractions.LogData.Methods;

/// <summary>
/// Denotes info about a <see cref="MethodBase"/>.
/// </summary>
public interface IMethodBaseInfo
{
   #region Properties
   /// <summary>The id of the <see cref="MemberInfo.DeclaringType"/>.</summary>
   /// <remarks>This id is only used within the log.</remarks>
   ulong DeclaringTypeId { get; }

   /// <summary>The list of infos about the <see cref="MethodBase.GetParameters"/>.</summary>
   IReadOnlyList<IParameterInfo> ParameterInfos { get; }

   /// <summary>The name of the method.</summary>
   string Name { get; }
   #endregion
}
