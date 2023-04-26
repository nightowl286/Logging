using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class ThreadComponentReadWriteTests : BinaryReadWriteTestsBase<ThreadComponentSerialiser, ThreadComponentDeserialiserLatest, IThreadComponent>
{
   #region Methods
   protected override IEnumerable<IThreadComponent> CreateData()
   {
      ThreadComponent component = new ThreadComponent(
         1,
         "name",
         ThreadState.Aborted,
         true,
         ThreadPriority.Highest,
         ApartmentState.MTA);

      yield return component;
   }
   protected override void Verify(IThreadComponent expected, IThreadComponent result)
   {
      Assert.That.AreEqual(expected.ManagedId, result.ManagedId);
      Assert.That.AreEqual(expected.Name, result.Name);
      Assert.That.AreEqual(expected.IsThreadPoolThread, result.IsThreadPoolThread);

      Assert.That.AreEqual(expected.State, result.State);
      Assert.That.AreEqual(expected.Priority, result.Priority);
      Assert.That.AreEqual(expected.ApartmentState, result.ApartmentState);
   }
   #endregion
}
