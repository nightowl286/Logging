using TNO.Logging.Common.Abstractions.LogData.Methods;
using ReflectionParameterInfo = System.Reflection.ParameterInfo;

namespace TNO.Logging.Common.LogData.Methods;

/// <summary>
/// Represents info about a <see cref="ReflectionParameterInfo"/>.
/// </summary>
/// <param name="TypeId">The id of the <see cref="ReflectionParameterInfo.ParameterType"/>.</param>
/// <param name="Modifier">The modifier of this parameter.</param>
/// <param name="HasDefaultValue">Whether this parameter has a default value.</param>
/// <param name="Name">The name of this parameter.</param>
public record class ParameterInfo(
   ulong TypeId,
   ParameterModifier Modifier,
   bool HasDefaultValue,
   string Name) : IParameterInfo
{
   #region Functions
   /// <summary>
   /// Creates a <see cref="ParameterInfo"/> for the given <paramref name="reflectionParameterInfo"/>.
   /// </summary>
   /// <param name="parameterTypeId">The id that will be assigned to the created <see cref="TypeId"/>.</param>
   /// <param name="reflectionParameterInfo">The parameter to create the <see cref="ParameterInfo"/> for.</param>
   /// <returns>The created <see cref="ParameterInfo"/>.</returns>
   public static ParameterInfo FromReflectionParameterInfo(ulong parameterTypeId, ReflectionParameterInfo reflectionParameterInfo)
   {
      string name = reflectionParameterInfo.Name ?? string.Empty;
      bool hasDefaultValue = reflectionParameterInfo.HasDefaultValue;
      ParameterModifier modifier = GetModifier(reflectionParameterInfo);

      return new ParameterInfo(parameterTypeId,
         modifier,
         hasDefaultValue,
         name);
   }
   private static ParameterModifier GetModifier(ReflectionParameterInfo parameterInfo)
   {
      if (parameterInfo.IsIn) return ParameterModifier.In;
      if (parameterInfo.IsOut) return ParameterModifier.Out;
      if (parameterInfo.ParameterType.IsByRef) return ParameterModifier.Ref;
      if (parameterInfo.IsDefined(typeof(ParamArrayAttribute), true)) return ParameterModifier.Params;

      return ParameterModifier.None;
   }
   #endregion
}
