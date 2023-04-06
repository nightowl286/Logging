using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Reading.Abstractions.LogData.AssemblyReferences;

namespace TNO.Logging.Reading.LogData.AssemblyReferences;

/// <summary>
/// A factory that should be used in instances of the see <see cref="IAssemblyReferenceDeserialiser"/>.
/// </summary>
internal static class AssemblyReferenceFactory
{
   #region Functions
   public static AssemblyReference Version0(ulong id, IAssemblyInfo assemblyInfo) => new AssemblyReference(assemblyInfo, id);
   #endregion
}
