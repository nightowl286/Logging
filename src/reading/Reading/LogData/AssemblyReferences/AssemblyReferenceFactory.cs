using TNO.Logging.Common.Abstractions.LogData.Assemblies;

namespace TNO.Logging.Reading.LogData.AssemblyReferences;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="AssemblyReference"/>.
/// </summary>
internal static class AssemblyReferenceFactory
{
   #region Functions
   public static AssemblyReference Version0(ulong id, IAssemblyInfo assemblyInfo) => new AssemblyReference(assemblyInfo, id);
   #endregion
}
