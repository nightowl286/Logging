using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class SimpleStackTraceComponentReadWriteTests : ReadWriteTestsBase<SimpleStackTraceComponentSerialiser, SimpleStackTraceComponentDeserialiserLatest, ISimpleStackTraceComponent>
{
   #region Methods
   protected override ISimpleStackTraceComponent CreateData()
   {
      int threadId = 5;
      string stackTrace = "some stack trace";
      SimpleStackTraceComponent component = new SimpleStackTraceComponent(stackTrace, threadId);

      return component;
   }
   protected override void Verify(ISimpleStackTraceComponent expected, ISimpleStackTraceComponent result)
   {
      Assert.That.AreEqual(expected.ThreadId, result.ThreadId);
      Assert.That.AreEqual(expected.StackTrace, result.StackTrace);
   }
   #endregion
}