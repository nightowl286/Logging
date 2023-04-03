using System.Globalization;
using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.LogData;
using TNO.Logging.Writing.Serialisers.LogData.Assemblies;

namespace TNO.ReadingWriting.Tests.LogData.Assemblies;

[TestClass]
public class AssemblyReferenceSerialiserReadWriteTest : ReadWriteTestBase<AssemblyReferenceSerialiser, AssemblyReferenceDeserialiserLatest, AssemblyReference>
{
   #region Methods
   protected override void Setup(out AssemblyReferenceSerialiser writer, out AssemblyReferenceDeserialiserLatest reader)
   {
      writer = new AssemblyReferenceSerialiser(
         new AssemblyInfoSerialiser());

      reader = new AssemblyReferenceDeserialiserLatest(
         new AssemblyInfoDeserialiserLatest());
   }

   protected override AssemblyReference CreateData()
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

      AssemblyReference assemblyReference = new AssemblyReference(assemblyInfo, 1);

      return assemblyReference;
   }

   protected override void Verify(AssemblyReference expected, AssemblyReference result)
   {
      Assert.That.AreEqual(expected.Id, result.Id);

      IAssemblyInfo expectedInfo = expected.AssemblyInfo;
      IAssemblyInfo resultInfo = result.AssemblyInfo;

      Assert.That.AreEqual(expectedInfo.Name, resultInfo.Name);
      Assert.That.AreEqual(expectedInfo.Version?.Major, resultInfo.Version?.Major);
      Assert.That.AreEqual(expectedInfo.Version?.Minor, resultInfo.Version?.Minor);
      Assert.That.AreEqual(expectedInfo.Version?.Build, resultInfo.Version?.Build);
      Assert.That.AreEqual(expectedInfo.Version?.Revision, resultInfo.Version?.Revision);
      Assert.That.AreEqual(expectedInfo.Culture?.Name, resultInfo.Culture?.Name);
      Assert.That.AreEqual(expectedInfo.DebuggingFlags, resultInfo.DebuggingFlags);
      Assert.That.AreEqual(expectedInfo.Configuration, resultInfo.Configuration);
      Assert.That.AreEqual(expectedInfo.PeKinds, resultInfo.PeKinds);
      Assert.That.AreEqual(expectedInfo.TargetPlatform, resultInfo.TargetPlatform);
   }
   #endregion
}
