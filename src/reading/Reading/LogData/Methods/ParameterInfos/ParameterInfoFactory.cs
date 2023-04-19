using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;

namespace TNO.Logging.Reading.LogData.Methods.ParameterInfos;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="IParameterInfo"/>.
/// </summary>
internal static class ParameterInfoFactory
{
   #region Functions
   public static IParameterInfo Version0(
      ulong typeId,
      ParameterModifier modifier,
      bool hasDefaultValue,
      string name)
   {
      return new ParameterInfo(
         typeId,
         modifier,
         hasDefaultValue,
         name);
   }
   #endregion
}
