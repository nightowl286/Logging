using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Common.LogData.StackTraces;
using TNO.Logging.Writing.Serialisers.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.StackTraces;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData.StackTraces;

[TestClass]
public class StackTraceInfoSerialiserCountTests : BinarySerialiserCountTestBase<StackTraceInfoSerialiser, IStackTraceInfo>
{
   #region Methods
   protected override StackTraceInfoSerialiser Setup()
   {
      ParameterInfoSerialiser parameterInfoSerialiser = new ParameterInfoSerialiser();

      return new StackTraceInfoSerialiser(
         new StackFrameInfoSerialiser(
            new MethodBaseInfoSerialiserDispatcher(
               new MethodInfoSerialiser(parameterInfoSerialiser),
               new ConstructorInfoSerialiser(parameterInfoSerialiser))));
   }
   #endregion

   #region Tests
   [TestMethod]
   public void Count_WithMinimalData()
   {
      // Arrange
      MethodInfo mainMethod = new MethodInfo(
        1,
        Array.Empty<IParameterInfo>(),
        "main method",
        1,
        Array.Empty<ulong>());

      StackFrameInfo stackFrameInfo = new StackFrameInfo(
         1,
         2,
         3,
         mainMethod,
         null);

      StackTraceInfo stackTraceInfo = new StackTraceInfo(
         1,
         new[] { stackFrameInfo });

      // Act + Assert
      CountTestBase(stackTraceInfo);
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

      StackTraceInfo stackTraceInfo = new StackTraceInfo(
         1,
         new[] { stackFrameInfo });

      // Act + Assert
      CountTestBase(stackTraceInfo);
   }
   #endregion
}
