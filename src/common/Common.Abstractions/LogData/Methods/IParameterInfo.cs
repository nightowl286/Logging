using System.Reflection;

namespace TNO.Logging.Common.Abstractions.LogData.Methods;

/// <summary>
/// Denotes info about a <see cref="ParameterInfo"/>.
/// </summary>
public interface IParameterInfo
{
   #region Properties
   /// <summary>The id of the <see cref="ParameterInfo.ParameterType"/>.</summary>
   /// <remarks>This id is only used within the log.</remarks>
   ulong TypeId { get; }

   /// <summary>The modifier of this parameter.</summary>
   ParameterModifier Modifier { get; }

   /// <summary>Whether this parameter has a default value.</summary>
   bool HasDefaultValue { get; }

   /// <summary>The name of this parameter.</summary>
   string Name { get; }
   #endregion
}
