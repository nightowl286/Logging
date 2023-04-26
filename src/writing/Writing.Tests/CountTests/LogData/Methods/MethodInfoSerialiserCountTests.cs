using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.Methods;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData.Methods;

[TestClass]
public class MethodInfoSerialiserCountTests : BinarySerialiserCountTestBase<MethodInfoSerialiser, IMethodInfo>
{
   #region Tests
   [TestMethod]
   public void Count_WithMinimalData()
   {
      // Arrange
      MethodInfo methodInfo = new MethodInfo(
         1,
         Array.Empty<IParameterInfo>(),
         "method",
         2,
         Array.Empty<ulong>());

      // Act + Assert
      CountTestBase(methodInfo);
   }

   [TestMethod]
   public void Count_WithMaximumData()
   {
      // Arrange
      ParameterInfo parameterA = new ParameterInfo(1, ParameterModifier.In, true, "parameterA");
      ParameterInfo parameterB = new ParameterInfo(2, ParameterModifier.Params, false, "parameterB");

      MethodInfo methodInfo = new MethodInfo(
         1,
         new[] { parameterA, parameterB },
         "method",
         2,
         new ulong[] { 1, 2, 3 });

      // Act + Assert
      CountTestBase(methodInfo);
   }
   #endregion
}
