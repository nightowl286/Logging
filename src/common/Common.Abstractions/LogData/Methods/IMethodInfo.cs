using System.Collections.Generic;
using System.Reflection;

namespace TNO.Logging.Common.Abstractions.LogData.Methods;

/// <summary>
/// Denotes info about a <see cref="MethodInfo"/>.
/// </summary>
public interface IMethodInfo : IMethodBaseInfo
{
   #region Properties
   /// <summary>The id of the <see cref="MethodInfo.ReturnType"/>.</summary>
   /// <remarks>This id is only used within the log.</remarks>
   ulong ReturnTypeId { get; }

   /// <summary>The ids of the <see cref="MethodInfo.GetGenericArguments"/>.</summary>
   /// <remarks>This id is only used within the log.</remarks>
   IReadOnlyList<ulong> GenericTypeIds { get; }
   #endregion
}
