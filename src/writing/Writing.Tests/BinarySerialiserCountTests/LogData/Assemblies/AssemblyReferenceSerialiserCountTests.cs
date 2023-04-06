using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.LogData;
using TNO.Logging.Writing.Serialisers.LogData.Assemblies;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData.Assemblies;

[TestClass]
public class AssemblyReferenceSerialiserCountTests : BinarySerialiserCountTestBase<AssemblyReferenceSerialiser, AssemblyReference>
{
   #region Methods
   protected override AssemblyReferenceSerialiser Setup()
   {
      return new AssemblyReferenceSerialiser(
         new AssemblyInfoSerialiser());
   }
   #endregion

   #region Tests
   [TestMethod]
   public void Count_WitMinimalData()
   {
      // Arrange
      AssemblyInfo assemblyInfo = new AssemblyInfo(
         null,
         null,
         null,
         default,
         string.Empty,
         null,
         string.Empty,
         default,
         default);

      AssemblyReference reference = new AssemblyReference(assemblyInfo, 1);

      // Act + Assert
      CountTestBase(reference);
   }

   [TestMethod]
   public void Count_WithMaximumData()
   {
      // Arrange
      AssemblyInfo assemblyInfo = new AssemblyInfo(
         "name",
         new Version(1, 2, 3, 4),
         CultureInfo.InvariantCulture,
         default,
         "location",
         DebuggableAttribute.DebuggingModes.DisableOptimizations,
         "configuration",
         PortableExecutableKinds.Preferred32Bit,
         ImageFileMachine.AMD64);

      AssemblyReference reference = new AssemblyReference(assemblyInfo, 1);

      // Act + Assert
      CountTestBase(reference);
   }
   #endregion
}
