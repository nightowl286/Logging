using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.LogData;

namespace TNO.Logging.Reading.LogData.AssemblyInfos;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="IAssemblyInfo"/>.
/// </summary>
internal static class AssemblyInfoFactory
{
   #region Functions
   public static IAssemblyInfo Version0(
      string? name,
      Version? version,
      CultureInfo? culture,
      AssemblyLocationKind locationKind,
      string location,
      DebuggableAttribute.DebuggingModes? debuggingFlags,
      string configuration,
      PortableExecutableKinds peKinds,
      ImageFileMachine targetPlatform)
   {
      AssemblyInfo assemblyInfo = new AssemblyInfo(
         name,
         version,
         culture,
         locationKind,
         location,
         debuggingFlags,
         configuration,
         peKinds,
         targetPlatform);

      return assemblyInfo;
   }
   #endregion
}
