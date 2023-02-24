using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class ThreadComponentReadWriteTest : ReadWriteTestBase<ThreadComponentSerialiser, ThreadComponentDeserialiserLatest, IThreadComponent>
{
   #region Methods
   protected override IThreadComponent CreateData()
   {
      ThreadComponent component = new ThreadComponent(
         1,
         "name",
         ThreadState.Aborted,
         true,
         ThreadPriority.Highest,
         ApartmentState.MTA);

      return component;
   }
   protected override void Verify(IThreadComponent expected, IThreadComponent result)
   {
      Assert.AreEqual(expected.ManagedId, result.ManagedId);
      Assert.AreEqual(expected.Name, result.Name);
      Assert.AreEqual(expected.IsThreadPoolThread, result.IsThreadPoolThread);

      Assert.AreEqual(expected.State, result.State);
      Assert.AreEqual(expected.Priority, result.Priority);
      Assert.AreEqual(expected.ApartmentState, result.ApartmentState);
   }
   #endregion
}
