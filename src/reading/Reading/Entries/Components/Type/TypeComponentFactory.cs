using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;

namespace TNO.Logging.Reading.Entries.Components.Type;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="ITypeComponent"/>.
/// </summary>
internal static class TypeComponentFactory
{
   #region Functions
   public static ITypeComponent Version0(ulong assemblyId) => new TypeComponent(assemblyId);
   #endregion
}