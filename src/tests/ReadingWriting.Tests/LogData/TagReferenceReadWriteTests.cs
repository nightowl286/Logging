using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.ReadingWriting.Tests.LogData;

[TestClass]
public class TagReferenceReadWriteTests : BinaryReadWriteTestsBase<TagReferenceSerialiser, TagReferenceDeserialiserLatest, TagReference>
{
   #region Methods
   protected override IEnumerable<TagReference> CreateData()
   {
      yield return new TagReference("tag", 5);
   }

   protected override void Verify(TagReference expected, TagReference result)
   {
      Assert.That.AreEqual(expected.Tag, result.Tag);
      Assert.That.AreEqual(expected.Id, result.Id);
   }
   #endregion
}