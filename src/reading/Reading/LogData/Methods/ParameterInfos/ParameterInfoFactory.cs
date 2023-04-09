using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Reading.Abstractions.LogData.ParameterInfos;

namespace TNO.Logging.Reading.LogData.ParameterInfos;

/// <summary>
/// A factory that should be used in instances of the see <see cref="IParameterInfoDeserialiser"/>.
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
