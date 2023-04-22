using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;

namespace TNO.Logging.Reading.LogData.AssemblyInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IAssemblyInfo"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.AssemblyInfo)]
public sealed class AssemblyInfoDeserialiser0 : IDeserialiser<IAssemblyInfo>
{
   #region Methods
   /// <inheritdoc/>
   public IAssemblyInfo Deserialise(BinaryReader reader)
   {
      string? name = reader.TryReadNullable(reader.ReadString);
      Version? version = null;
      if (reader.ReadBoolean())
      {
         int major = reader.ReadInt32();
         int minor = reader.ReadInt32();
         int build = reader.ReadInt32();
         int revision = reader.ReadInt32();

         version = new Version(major, minor, build, revision);
      }

      string? cultureName = reader.TryReadNullable(reader.ReadString);
      CultureInfo? culture = cultureName is null ? null : new CultureInfo(cultureName);

      byte rawLocationKind = reader.ReadByte();
      string location = reader.ReadString();

      int? rawDebuggingFlags = reader.TryReadNullable(reader.ReadInt32);
      string configuration = reader.ReadString();
      int rawPeKinds = reader.ReadInt32();
      int rawTargetPlatform = reader.ReadInt32();

      AssemblyLocationKind locationKind = (AssemblyLocationKind)rawLocationKind;
      DebuggableAttribute.DebuggingModes? debuggingFlags = (DebuggableAttribute.DebuggingModes?)rawDebuggingFlags;
      PortableExecutableKinds peKinds = (PortableExecutableKinds)rawPeKinds;
      ImageFileMachine targetPlatform = (ImageFileMachine)rawTargetPlatform;

      return AssemblyInfoFactory.Version0(
         name,
         version,
         culture,
         locationKind,
         location,
         debuggingFlags,
         configuration,
         peKinds,
         targetPlatform);
   }
   #endregion
}
