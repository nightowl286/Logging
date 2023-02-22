using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class TagComponentReadWriteTest : ReadWriteTestBase<TagComponentSerialiser, TagComponentDeserialiserLatest, ITagComponent>
{
   #region Methods
   protected override ITagComponent CreateData()
   {
      ulong tagId = 5;
      TagComponent component = new TagComponent(tagId);

      return component;
   }
   protected override void Verify(ITagComponent expected, ITagComponent result)
   {
      Assert.AreEqual(expected.TagId, result.TagId);
   }
   #endregion
}