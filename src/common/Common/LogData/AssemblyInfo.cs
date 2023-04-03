using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;

namespace TNO.Logging.Common.LogData;

/// <summary>
/// Represents info about an <see cref="Assembly"/>.
/// </summary>
/// <param name="Name">The name of the assembly.</param>
/// <param name="Version">The version of the assembly.</param>
/// <param name="Culture">The culture supported by the assembly.</param>
/// <param name="LocationKind">kind of the location where the assembly is location.</param>
/// <param name="Location">The location where the assembly is located.</param>
/// <param name="DebuggingFlags">The debugging flags associated with this assembly.</param>
/// <param name="Configuration">The configuration in which the assembly was compiled.</param>
/// <param name="PeKinds">The nature of the code in the <see cref="Assembly.ManifestModule"/>.</param>
/// <param name="TargetPlatform">The platform targeted by the <see cref="Assembly.ManifestModule"/>.</param>
public record class AssemblyInfo(
   string? Name,
   Version? Version,
   CultureInfo? Culture,
   AssemblyLocationKind LocationKind,
   string Location,
   DebuggableAttribute.DebuggingModes? DebuggingFlags,
   string Configuration,
   PortableExecutableKinds PeKinds,
   ImageFileMachine TargetPlatform) : IAssemblyInfo
{
   #region Functions
   /// <summary>Creates an <see cref="AssemblyInfo"/> for the given <paramref name="assembly"/>.</summary>
   /// <param name="assembly">The assembly to create the <see cref="AssemblyInfo"/> for.</param>
   /// <returns>The created <see cref="AssemblyInfo"/>.</returns>
   public static AssemblyInfo FromAssembly(Assembly assembly)
   {
      AssemblyName assemblyName = assembly.GetName();
      string location = AssemblyLocationResolver.Instance.GetLocation(assembly.Location, out AssemblyLocationKind locationKind);
      DebuggableAttribute.DebuggingModes? debuggingModes = assembly.GetCustomAttribute<DebuggableAttribute>()?.DebuggingFlags;
      string configuration = assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration ?? string.Empty;
      assembly.ManifestModule.GetPEKind(out PortableExecutableKinds peKinds, out ImageFileMachine targetMachine);

      return new AssemblyInfo(
         assemblyName.Name,
         assemblyName.Version,
         assemblyName.CultureInfo,
         locationKind, location,
         debuggingModes,
         configuration,
         peKinds,
         targetMachine);
   }
   #endregion
}