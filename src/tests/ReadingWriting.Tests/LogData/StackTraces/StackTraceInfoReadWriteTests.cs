using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Common.LogData.StackTraces;
using TNO.Logging.Reading.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.StackTraces;

namespace TNO.ReadingWriting.Tests.LogData.StackTraces;

[TestClass]
public class StackTraceInfoReadWriteTests : ReadWriteTestsBase<StackTraceInfoSerialiser, StackTraceInfoDeserialiserLatest, IStackTraceInfo>
{
   #region Methods
   protected override void Setup(out StackTraceInfoSerialiser writer, out StackTraceInfoDeserialiserLatest reader)
   {
      ParameterInfoSerialiser parameterInfoSerialiser = new ParameterInfoSerialiser();
      writer =
         new StackTraceInfoSerialiser(
            new StackFrameInfoSerialiser(
               new MethodBaseInfoSerialiserDispatcher(
                  new MethodInfoSerialiser(parameterInfoSerialiser),
                  new ConstructorInfoSerialiser(parameterInfoSerialiser))));

      ParameterInfoDeserialiserLatest parameterInfoDeserialiser = new ParameterInfoDeserialiserLatest();
      reader =
         new StackTraceInfoDeserialiserLatest(
            new StackFrameInfoDeserialiserLatest(
               new MethodBaseInfoDeserialiserDispatcher(
                  new MethodInfoDeserialiserLatest(parameterInfoDeserialiser),
                  new ConstructorInfoDeserialiserLatest(parameterInfoDeserialiser))));
   }

   protected override IStackTraceInfo CreateData()
   {
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

      return stackTraceInfo;
   }

   protected override void Verify(IStackTraceInfo expected, IStackTraceInfo result)
   {
      Assert.That.AreEqual(expected.ThreadId, result.ThreadId);
      Assert.That.AreEqual(expected.Frames.Count, result.Frames.Count);

      for (int i = 0; i < expected.Frames.Count; i++)
         Verify(expected.Frames[i], result.Frames[i]);
   }

   private static void Verify(IStackFrameInfo expected, IStackFrameInfo result)
   {
      Assert.That.AreEqual(expected.FileId, result.FileId);
      Assert.That.AreEqual(expected.LineInFile, result.LineInFile);
      Assert.That.AreEqual(expected.ColumnInLine, result.ColumnInLine);
      Verify(expected.MainMethod, result.MainMethod);
      Verify(expected.SecondaryMethod, result.SecondaryMethod);
   }

   private static void Verify(IMethodBaseInfo? expected, IMethodBaseInfo? result)
   {
      bool isExpectedNull = expected is null;
      bool isResultNull = result is null;

      Assert.That.AreEqual(isExpectedNull, isResultNull);

      if (expected is not null && result is not null)
      {
         Assert.That.AreEqual(expected.Name, result.Name);
         Assert.That.AreEqual(expected.DeclaringTypeId, result.DeclaringTypeId);
         Assert.That.AreEqual(expected.ParameterInfos.Count, result.ParameterInfos.Count);
      }
   }
   #endregion
}
