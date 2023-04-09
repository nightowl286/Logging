﻿using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class TagComponentReadWriteTests : ReadWriteTestsBase<TagComponentSerialiser, TagComponentDeserialiserLatest, ITagComponent>
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
      Assert.That.AreEqual(expected.TagId, result.TagId);
   }
   #endregion
}