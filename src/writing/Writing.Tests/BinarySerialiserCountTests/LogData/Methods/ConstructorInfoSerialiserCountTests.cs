using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.Constructors;
using TNO.Logging.Writing.Serialisers.LogData.Methods;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData.Constructors;

[TestClass]
public class ConstructorInfoSerialiserCountTests : BinarySerialiserCountTestBase<ConstructorInfoSerialiser, IConstructorInfo>
{
   #region Constructors
   protected override ConstructorInfoSerialiser Setup()
   {
      return new ConstructorInfoSerialiser(
         new ParameterInfoSerialiser());
   }
   #endregion

   #region Tests
   [TestMethod]
   public void Count_WithMinimalData()
   {
      // Arrange
      ConstructorInfo constructorInfo = new ConstructorInfo(
         1,
         Array.Empty<IParameterInfo>(),
         "constructor");

      // Act + Assert
      CountTestBase(constructorInfo);
   }

   [TestMethod]
   public void Count_WithMaximumData()
   {
      // Arrange
      ParameterInfo parameterA = new ParameterInfo(1, ParameterModifier.In, true, "parameterA");
      ParameterInfo parameterB = new ParameterInfo(2, ParameterModifier.Params, false, "parameterB");

      ConstructorInfo constructorInfo = new ConstructorInfo(
         1,
         new[] { parameterA, parameterB },
         "constructor");

      // Act + Assert
      CountTestBase(constructorInfo);
   }
   #endregion
}
