using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData.Assemblies;

/// <summary>
/// A serialiser for <see cref="IAssemblyInfo"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.AssemblyInfo)]
public class AssemblyInfoSerialiser : ISerialiser<IAssemblyInfo>
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IAssemblyInfo data)
   {
      string? name = data.Name;
      Version? version = data.Version;
      CultureInfo? culture = data.Culture;
      AssemblyLocationKind locationKind = data.LocationKind;
      string location = data.Location;
      DebuggableAttribute.DebuggingModes? debuggingFlags = data.DebuggingFlags;
      string configuration = data.Configuration;
      PortableExecutableKinds peKinds = data.PeKinds;
      ImageFileMachine targetPlatform = data.TargetPlatform;

      byte rawLocationKind = (byte)locationKind;
      int? rawDebuggingFlags = (int?)debuggingFlags;
      int rawPeKinds = (int)peKinds;
      int rawTargetPlatform = (int)targetPlatform;

      if (writer.TryWriteNullable(name))
         writer.Write(name);

      if (writer.TryWriteNullable(version))
      {
         writer.Write(version.Major);
         writer.Write(version.Minor);
         writer.Write(version.Build);
         writer.Write(version.Revision);
      }

      if (writer.TryWriteNullable(culture))
         writer.Write(culture.Name);

      writer.Write(rawLocationKind);
      writer.Write(location);

      if (writer.TryWriteNullable(rawDebuggingFlags))
         writer.Write(rawDebuggingFlags.Value);

      writer.Write(configuration);
      writer.Write(rawPeKinds);
      writer.Write(rawTargetPlatform);
   }

   /// <inheritdoc/>
   public int Count(IAssemblyInfo data)
   {
      int size =
         (sizeof(int) * 2) +
         (sizeof(byte) * 5);

      if (data.Version is not null)
         size += sizeof(int) * 4;

      if (data.DebuggingFlags is not null)
         size += sizeof(int);

      int locationSize = BinaryWriterSizeHelper.StringSize(data.Location);
      int nameSize = BinaryWriterSizeHelper.StringSize(data.Name);
      int cultureSize = BinaryWriterSizeHelper.StringSize(data.Culture?.Name);
      int configurationSize = BinaryWriterSizeHelper.StringSize(data.Configuration);

      return size + locationSize + nameSize + cultureSize + configurationSize;
   }
   #endregion
}
