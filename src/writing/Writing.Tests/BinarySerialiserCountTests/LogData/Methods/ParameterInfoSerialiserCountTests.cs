using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.Methods;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData.Methods;

[TestClass]
public class ParameterInfoSerialiserCountTests : BinarySerialiserCountTestBase<ParameterInfoSerialiser, IParameterInfo>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      ParameterInfo parameterInfo = new ParameterInfo(
         1,
         ParameterModifier.Params,
         true,
         "parameter");

      // Act + Assert
      CountTestBase(parameterInfo);
   }
   #endregion
}
