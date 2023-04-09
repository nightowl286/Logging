namespace TNO.Logging.Common.Abstractions.LogData.Methods;

/// <summary>
/// Represents the modifier of the parameter.
/// </summary>
public enum ParameterModifier : byte
{
   /// <summary>Means that the parameter has no special modifiers.</summary>
   None,

   /// <summary>Means that the parameter is passed in by reference and cannot be modified.</summary>
   In,

   /// <summary>Means that the parameter is passed out by reference, and must be assigned by the method.</summary>
   Out,

   /// <summary>Means that the parameter is passed by reference, it can be read and modified by the method.</summary>
   Ref,

   /// <summary>Means that the parameter accepts a variable number of arguments.</summary>
   Params
}
