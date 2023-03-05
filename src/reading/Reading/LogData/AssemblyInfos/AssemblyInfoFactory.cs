using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.LogData;
using TNO.Logging.Reading.Abstractions.LogData.AssemblyInfos;

namespace TNO.Logging.Reading.LogData.AssemblyInfos;

/// <summary>
/// A factory that should be used in instances of the see <see cref="IAssemblyInfoDeserialiser"/>.
/// </summary>
internal static class AssemblyInfoFactory
{
   #region Functions
   public static IAssemblyInfo Version0(
      ulong id,
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
         id,
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
