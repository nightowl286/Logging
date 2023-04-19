using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;

namespace TNO.Logging.Reading.LogData.Methods.MethodInfos;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="IMethodInfo"/>.
/// </summary>
internal static class MethodInfoFactory
{
   #region Functions
   public static IMethodInfo Version0(
      ulong declaringTypeId,
      IReadOnlyList<IParameterInfo> parameterInfos,
      string name,
      ulong returnTypeId,
      IReadOnlyList<ulong> genericTypeIds)
   {
      return new MethodInfo(
         declaringTypeId,
         parameterInfos,
         name,
         returnTypeId,
         genericTypeIds);
   }
   #endregion
}
