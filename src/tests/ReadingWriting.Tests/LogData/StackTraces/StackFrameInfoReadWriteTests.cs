﻿using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Common.LogData.StackTraces;
using TNO.Logging.Writing.Serialisers.LogData.StackTraces;

namespace TNO.ReadingWriting.Tests.LogData.StackTraces;

[TestClass]
public class StackFrameInfoReadWriteTests : BinaryReadWriteTestsBase<StackFrameInfoSerialiser, StackFrameInfoDeserialiserLatest, IStackFrameInfo>
{
   #region Methods
   protected override void Setup(out StackFrameInfoSerialiser writer, out StackFrameInfoDeserialiserLatest reader)
   {
      writer = new StackFrameInfoSerialiser(GeneralSerialiser.Instance);
      reader = new StackFrameInfoDeserialiserLatest(GeneralDeserialiser.Instance);
   }

   protected override IEnumerable<Annotated<IStackFrameInfo>> CreateData()
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

      {
         StackFrameInfo stackFrameInfo = new StackFrameInfo(
            1,
            2,
            3,
            mainMethod,
            secondaryMethod);

         yield return new(stackFrameInfo);
      }

      {
         StackFrameInfo stackFrameInfo = new StackFrameInfo(
            1,
            2,
            3,
            mainMethod,
            null);

         yield return new(stackFrameInfo, "No secondary method");
      }
   }

   protected override void Verify(IStackFrameInfo expected, IStackFrameInfo result)
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
