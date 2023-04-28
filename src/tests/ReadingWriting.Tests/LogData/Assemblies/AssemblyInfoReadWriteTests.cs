using System.Globalization;
using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.LogData;
using TNO.Logging.Writing.Serialisers.LogData.Assemblies;

namespace TNO.ReadingWriting.Tests.LogData.Assemblies;

[TestClass]
public class AssemblyInfoReadWriteTests : BinaryReadWriteTestsBase<AssemblyInfoSerialiser, AssemblyInfoDeserialiserLatest, IAssemblyInfo>
{
   #region Methods
   protected override IEnumerable<Annotated<IAssemblyInfo>> CreateData()
   {
      {
         AssemblyInfo assemblyInfo = new AssemblyInfo(
            "name",
            new Version(1, 2, 3, 4),
            CultureInfo.InvariantCulture,
            AssemblyLocationKind.External,
            "location",
            System.Diagnostics.DebuggableAttribute.DebuggingModes.DisableOptimizations,
            "configuration",
            PortableExecutableKinds.Preferred32Bit,
            ImageFileMachine.AMD64);

         yield return new(assemblyInfo, "Full data");
      }

      {
         AssemblyInfo assemblyInfo = new AssemblyInfo(
            null,
            null,
            null,
            AssemblyLocationKind.External,
            "location",
            null,
            "configuration",
            PortableExecutableKinds.Preferred32Bit,
            ImageFileMachine.AMD64);

         yield return new(assemblyInfo, "Minimal data");
      }
   }

   protected override void Verify(IAssemblyInfo expected, IAssemblyInfo result)
   {
      Assert.That.AreEqual(expected.Name, result.Name);
      Assert.That.AreEqual(expected.Version?.Major, result.Version?.Major);
      Assert.That.AreEqual(expected.Version?.Minor, result.Version?.Minor);
      Assert.That.AreEqual(expected.Version?.Build, result.Version?.Build);
      Assert.That.AreEqual(expected.Version?.Revision, result.Version?.Revision);
      Assert.That.AreEqual(expected.Culture?.Name, result.Culture?.Name);
      Assert.That.AreEqual(expected.DebuggingFlags, result.DebuggingFlags);
      Assert.That.AreEqual(expected.Configuration, result.Configuration);
      Assert.That.AreEqual(expected.PeKinds, result.PeKinds);
      Assert.That.AreEqual(expected.TargetPlatform, result.TargetPlatform);
   }
   #endregion
}
