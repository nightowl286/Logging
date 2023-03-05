using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.LogData;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData;

[TestClass]
public class AssemblyInfoSerialiserCountTests : BinarySerialiserCountTestBase<AssemblyInfoSerialiser, IAssemblyInfo>
{
   #region Tests
   [TestMethod]
   public void Count_WitMinimalData()
   {
      // Arrange
      AssemblyInfo assemblyInfo = new AssemblyInfo(
         0,
         null,
         null,
         null,
         default,
         string.Empty,
         null,
         string.Empty,
         default,
         default);

      // Act + Verify
      CountTestBase(assemblyInfo);
   }

   [TestMethod]
   public void Count_WithMaximumData()
   {
      // Arrange
      AssemblyInfo assemblyInfo = new AssemblyInfo(
         0,
         "name",
         new Version(1, 2, 3, 4),
         CultureInfo.InvariantCulture,
         default,
         "location",
         DebuggableAttribute.DebuggingModes.DisableOptimizations,
         "configuration",
         PortableExecutableKinds.Preferred32Bit,
         ImageFileMachine.AMD64);

      // Act + Verify
      CountTestBase(assemblyInfo);
   }
   #endregion
}
