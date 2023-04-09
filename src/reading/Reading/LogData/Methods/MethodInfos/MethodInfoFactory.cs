using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Reading.Abstractions.LogData.MethodInfos;

namespace TNO.Logging.Reading.LogData.MethodInfos;

/// <summary>
/// A factory that should be used in instances of the see <see cref="IMethodInfoDeserialiser"/>.
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
