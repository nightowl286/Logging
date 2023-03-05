using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;

namespace TNO.Logging.Common.LogData;

/// <summary>
/// Represents info about an <see cref="Assembly"/>.
/// </summary>
/// <param name="Id">The id of the assembly.</param>
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
   ulong Id,
   string? Name,
   Version? Version,
   CultureInfo? Culture,
   AssemblyLocationKind LocationKind,
   string Location,
   DebuggableAttribute.DebuggingModes? DebuggingFlags,
   string Configuration,
   PortableExecutableKinds PeKinds,
   ImageFileMachine TargetPlatform) : IAssemblyInfo;