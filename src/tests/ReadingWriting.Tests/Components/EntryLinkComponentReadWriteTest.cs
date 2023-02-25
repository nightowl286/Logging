using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class EntryLinkComponentReadWriteTest : ReadWriteTestBase<EntryLinkComponentSerialiser, EntryLinkComponentDeserialiserLatest, IEntryLinkComponent>
{
   #region Methods
   protected override IEntryLinkComponent CreateData()
   {
      ulong EntryLinkId = 5;
      EntryLinkComponent component = new EntryLinkComponent(EntryLinkId);

      return component;
   }
   protected override void Verify(IEntryLinkComponent expected, IEntryLinkComponent result)
   {
      Assert.That.AreEqual(expected.EntryId, result.EntryId);
   }
   #endregion
}