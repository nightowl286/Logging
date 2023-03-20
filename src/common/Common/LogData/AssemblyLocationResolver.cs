using System.Reflection;
using System.Runtime.InteropServices;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;

namespace TNO.Logging.Common.LogData;

/// <summary>
/// Represents a resolver for the <see cref="Assembly.Location"/>.
/// </summary>
public class AssemblyLocationResolver
{
   #region Fields
   private readonly string? _entryLocation;
   private readonly string? _runtimePath;

   // not a perfect approach, but figuring out the actual case-sensitivity would be more error prone.
   private static readonly StringComparison StringComparison = StringComparison.OrdinalIgnoreCase;
   #endregion

   #region Properties
   /// <summary>The singleton instance of the <see cref="AssemblyLocationResolver"/>.</summary>
   public static AssemblyLocationResolver Instance { get; } = new AssemblyLocationResolver();
   #endregion

   #region Constructor
   private AssemblyLocationResolver()
   {
      _entryLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
      _runtimePath = RuntimeEnvironment.GetRuntimeDirectory();

      // should be default dotnet runtime directory
      if (_runtimePath != AppDomain.CurrentDomain.BaseDirectory)
      {
         // get root runtime directory, and not just to a specific runtime, as an application isn't limited to assemblies from just one runtime location.
         // https://learn.microsoft.com/en-us/dotnet/core/install/how-to-detect-installed-versions?pivots=os-windows#check-for-install-folders
         _runtimePath = Path.GetDirectoryName(Path.GetDirectoryName(_runtimePath));
      }
   }
   #endregion

   #region Methods
   /// <summary>Gets the relative location of the given <paramref name="assembly"/>.</summary>
   /// <param name="assembly">The assembly to get the relative location of.</param>
   /// <param name="locationKind">The kind of the returned relative location.</param>
   /// <returns>The relative location of the given <paramref name="assembly"/>.</returns>
   public string GetLocation(Assembly assembly, out AssemblyLocationKind locationKind)
      => GetLocation(assembly.Location, out locationKind);

   /// <summary>Gets the relative location of the given <paramref name="assemblyPath"/>.</summary>
   /// <param name="assemblyPath">The full path of the assembly.</param>
   /// <param name="locationKind">The kind of the returned relative location.</param>
   /// <returns>The relative location of the given <paramref name="assemblyPath"/>.</returns>
   public string GetLocation(string assemblyPath, out AssemblyLocationKind locationKind)
   {
      if (string.IsNullOrWhiteSpace(assemblyPath))
      {
         locationKind = AssemblyLocationKind.Unknown;
         return assemblyPath;
      }

      if (_runtimePath is not null && assemblyPath.StartsWith(_runtimePath, StringComparison))
      {
         locationKind = AssemblyLocationKind.SharedRuntime;
         return Path.GetRelativePath(_runtimePath, assemblyPath);
      }
      else if (_entryLocation is not null && assemblyPath.StartsWith(_entryLocation, StringComparison))
      {
         locationKind = AssemblyLocationKind.Application;
         return Path.GetRelativePath(_entryLocation, assemblyPath);
      }
      else if (Path.IsPathFullyQualified(assemblyPath))
      {
         locationKind = AssemblyLocationKind.External;
         return assemblyPath;
      }

      locationKind = AssemblyLocationKind.Unknown;
      return string.Empty;
   }
   #endregion
}
