using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.Constructors;
using TNO.Logging.Writing.Serialisers.LogData.Methods;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData.Methods;

[TestClass]
public class MethodBaseInfoSerialiserDispatcherCountTests : BinarySerialiserCountTestBase<MethodBaseInfoSerialiserDispatcher, IMethodBaseInfo>
{
   #region Methods
   protected override MethodBaseInfoSerialiserDispatcher Setup()
   {
      ParameterInfoSerialiser parameterInfoSerialiser = new ParameterInfoSerialiser();

      return new MethodBaseInfoSerialiserDispatcher(
         new MethodInfoSerialiser(parameterInfoSerialiser),
         new ConstructorInfoSerialiser(parameterInfoSerialiser));
   }
   #endregion

   #region Tests
   [TestMethod]
   public void Count_WithMethodInfo()
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
   public void Count_WithConstructorInfo()
   {
      // Arrange
      ConstructorInfo constructorInfo = new ConstructorInfo(
         1,
         Array.Empty<IParameterInfo>(),
         "constructor");

      // Act + Assert
      CountTestBase(constructorInfo);
   }
   #endregion
}
