using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Reading.Abstractions.LogData.Methods.ConstructorInfos;

namespace TNO.Logging.Reading.LogData.Methods.ConstructorInfos;

/// <summary>
/// A factory that should be used in instances of the see <see cref="IConstructorInfoDeserialiser"/>.
/// </summary>
internal static class ConstructorInfoFactory
{
   #region Functions
   public static IConstructorInfo Version0(
      ulong declaringTypeId,
      IReadOnlyList<IParameterInfo> parameterInfos,
      string name)
   {
      return new ConstructorInfo(
         declaringTypeId,
         parameterInfos,
         name);
   }
   #endregion
}
