using System.Reflection;

namespace TNO.Logging.Common.Abstractions.LogData.Methods;

/// <summary>
/// Represents the kind of the <see cref="MethodBase"/>.
/// </summary>
public enum MethodKind : byte
{
   /// <summary>Represents a <see cref="MethodInfo"/>.</summary>
   Method,

   /// <summary>Represents a <see cref="ConstructorInfo"/>.</summary>
   Constructor,
}
