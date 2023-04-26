using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using static System.Diagnostics.DebuggableAttribute;

namespace TNO.Logging.Common.LogData;

/// <summary>
/// Represents info about an <see cref="Assembly"/>.
/// </summary>

public class AssemblyInfo : IAssemblyInfo
{
   #region Properties
   /// <inheritdoc/>
   public string? Name { get; }

   /// <inheritdoc/>
   public Version? Version { get; }

   /// <inheritdoc/>
   public CultureInfo? Culture { get; }

   /// <inheritdoc/>
   public AssemblyLocationKind LocationKind { get; }

   /// <inheritdoc/>
   public string Location { get; }

   /// <inheritdoc/>
   public DebuggingModes? DebuggingFlags { get; }

   /// <inheritdoc/>
   public string Configuration { get; }

   /// <inheritdoc/>
   public PortableExecutableKinds PeKinds { get; }

   /// <inheritdoc/>
   public ImageFileMachine TargetPlatform { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="AssemblyInfo"/>.</summary>
   /// <param name="name">The name of the assembly.</param>
   /// <param name="version">The version of the assembly.</param>
   /// <param name="culture">The culture supported by the assembly.</param>
   /// <param name="locationKind">kind of the location where the assembly is location.</param>
   /// <param name="location">The location where the assembly is located.</param>
   /// <param name="debuggingFlags">The debugging flags associated with this assembly.</param>
   /// <param name="configuration">The configuration in which the assembly was compiled.</param>
   /// <param name="peKinds">The nature of the code in the <see cref="Assembly.ManifestModule"/>.</param>
   /// <param name="targetPlatform">The platform targeted by the <see cref="Assembly.ManifestModule"/>.</param>
   public AssemblyInfo(
      string? name,
      Version? version,
      CultureInfo? culture,
      AssemblyLocationKind locationKind,
      string location,
      DebuggingModes? debuggingFlags,
      string configuration,
      PortableExecutableKinds peKinds,
      ImageFileMachine targetPlatform)
   {
      Name = name;
      Version = version;
      Culture = culture;
      LocationKind = locationKind;
      Location = location;
      DebuggingFlags = debuggingFlags;
      Configuration = configuration;
      PeKinds = peKinds;
      TargetPlatform = targetPlatform;
   }
   #endregion

   #region Functions
   /// <summary>Creates an <see cref="AssemblyInfo"/> for the given <paramref name="assembly"/>.</summary>
   /// <param name="assembly">The assembly to create the <see cref="AssemblyInfo"/> for.</param>
   /// <returns>The created <see cref="AssemblyInfo"/>.</returns>
   public static AssemblyInfo FromAssembly(Assembly assembly)
   {
      AssemblyName assemblyName = assembly.GetName();
      string location = AssemblyLocationResolver.Instance.GetLocation(assembly.Location, out AssemblyLocationKind locationKind);
      DebuggingModes? debuggingModes = assembly.GetCustomAttribute<DebuggableAttribute>()?.DebuggingFlags;
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