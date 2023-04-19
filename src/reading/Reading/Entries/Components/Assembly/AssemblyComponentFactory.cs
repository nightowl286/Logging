using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;

namespace TNO.Logging.Reading.Entries.Components.Assembly;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="IAssemblyComponent"/>.
/// </summary>
internal static class AssemblyComponentFactory
{
   #region Functions
   public static IAssemblyComponent Version0(ulong assemblyId) => new AssemblyComponent(assemblyId);
   #endregion
}