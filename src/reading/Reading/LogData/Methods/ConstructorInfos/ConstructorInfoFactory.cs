using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;

namespace TNO.Logging.Reading.LogData.Methods.ConstructorInfos;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="IConstructorInfo"/>.
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
