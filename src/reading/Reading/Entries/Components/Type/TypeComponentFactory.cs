using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Type;

namespace TNO.Logging.Reading.Entries.Components.Type;

/// <summary>
/// A factory class that should be used in instances of the <see cref="ITypeComponentDeserialiser"/>.
/// </summary>
internal static class TypeComponentFactory
{
   #region Functions
   public static ITypeComponent Version0(ulong assemblyId) => new TypeComponent(assemblyId);
   #endregion
}