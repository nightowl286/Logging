﻿using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Common.LogData.StackTraces;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class StackTraceComponentReadWriteTests : BinaryReadWriteTestsBase<StackTraceComponentSerialiser, StackTraceComponentDeserialiserLatest, IStackTraceComponent>
{
   #region Methods
   protected override void Setup(out StackTraceComponentSerialiser writer, out StackTraceComponentDeserialiserLatest reader)
   {
      writer = new StackTraceComponentSerialiser(GeneralSerialiser.Instance);
      reader = new StackTraceComponentDeserialiserLatest(GeneralDeserialiser.Instance);
   }

   protected override IEnumerable<Annotated<IStackTraceComponent>> CreateData()
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

         StackTraceInfo stackTraceInfo = new StackTraceInfo(
            1,
            new[] { stackFrameInfo });

         StackTraceComponent component = new StackTraceComponent(stackTraceInfo);

         yield return new(component);
      }

      {
         StackFrameInfo stackFrameInfo = new StackFrameInfo(
            1,
            2,
            3,
            mainMethod,
            null);

         StackTraceInfo stackTraceInfo = new StackTraceInfo(
            1,
            new[] { stackFrameInfo });

         StackTraceComponent component = new StackTraceComponent(stackTraceInfo);

         yield return new(component, "No secondary method");
      }
   }

   protected override void Verify(IStackTraceComponent expected, IStackTraceComponent result)
   {
      IStackTraceInfo expectedInfo = expected.StackTrace;
      IStackTraceInfo resultInfo = result.StackTrace;

      Verify(expectedInfo, resultInfo);
   }

   private static void Verify(IStackTraceInfo expected, IStackTraceInfo result)
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