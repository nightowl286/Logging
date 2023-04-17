using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Common.LogData.StackTraces;
using TNO.Logging.Writing.Serialisers.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.StackTraces;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData.StackTraces;

[TestClass]
public class StackFrameInfoSerialiserCountTests : BinarySerialiserCountTestBase<StackFrameInfoSerialiser, IStackFrameInfo>
{
   #region Methods
   protected override StackFrameInfoSerialiser Setup()
   {
      ParameterInfoSerialiser parameterInfoSerialiser = new ParameterInfoSerialiser();

      return new StackFrameInfoSerialiser(
         new MethodBaseInfoSerialiserDispatcher(
            new MethodInfoSerialiser(parameterInfoSerialiser),
            new ConstructorInfoSerialiser(parameterInfoSerialiser)));
   }
   #endregion

   #region Tests
   [TestMethod]
   public void Count_WithMinimalData()
   {
      MethodInfo mainMethod = new MethodInfo(
        1,
        Array.Empty<IParameterInfo>(),
        "main method",
        1,
        Array.Empty<ulong>());

      // Arrange
      StackFrameInfo stackFrameInfo = new StackFrameInfo(
         1,
         2,
         3,
         mainMethod,
         null);

      // Act + Assert
      CountTestBase(stackFrameInfo);
   }

   [TestMethod]
   public void Count_WithMaximumData()
   {
      // Arrange
      MethodInfo mainMethod = new MethodInfo(
         1,
         Array.Empty<IParameterInfo>(),
         "main method",
         1,
         Array.Empty<ulong>());

      ConstructorInfo secondaryMethod = new ConstructorInfo(
         1,
         Array.Empty<IParameterInfo>(),
         "secondary method");

      StackFrameInfo stackFrameInfo = new StackFrameInfo(
         1,
         2,
         3,
         mainMethod,
         secondaryMethod);

      // Act + Assert
      CountTestBase(stackFrameInfo);
   }
   #endregion
}
