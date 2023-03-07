using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Assembly;

namespace TNO.Logging.Reading.Entries.Components.Assembly;

/// <summary>
/// A factory class that should be used instances of the <see cref="IAssemblyComponentDeserialiser"/>.
/// </summary>
internal static class AssemblyComponentFactory
{
   #region Functions
   public static IAssemblyComponent Version0(ulong assemblyId) => new AssemblyComponent(assemblyId);
   #endregion
}