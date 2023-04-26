using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Reading.LogData.General;
using TNO.Logging.Writing.Serialisers.LogData.General;

namespace TNO.ReadingWriting.Tests.LogData.General;

[TestClass]
public class CollectionInfoReadWriteTests : BinaryReadWriteTestsBase<CollectionInfoSerialiser, CollectionInfoDeserialiser, ICollectionInfo>
{
   #region Methods
   protected override void Setup(out CollectionInfoSerialiser writer, out CollectionInfoDeserialiser reader)
   {
      writer = (CollectionInfoSerialiser)GeneralSerialiser.Instance.Get<ICollectionInfo>();
      reader = (CollectionInfoDeserialiser)GeneralDeserialiser.Instance.Get<ICollectionInfo>();
   }

   protected override IEnumerable<ICollectionInfo> CreateData()
   {
      foreach (object? value in ValidPrimitiveValues.Values)
         yield return CreateCollection(value);

      yield return CreateCollection(ValidPrimitiveValues.Values);
   }
   protected override void Verify(ICollectionInfo expected, ICollectionInfo result)
   {
      IReadOnlyCollection<object?> expectedCollection = expected.Collection;
      IReadOnlyCollection<object?> resultCollection = result.Collection;

      Assert.That.AreEqual(expectedCollection.Count, resultCollection.Count);
      CollectionAssert.AreEquivalent(expectedCollection.ToArray(), resultCollection.ToArray());
   }
   #endregion

   #region Helpers
   private static ICollectionInfo CreateCollection(object? value) => CreateCollection(new object?[] { value });
   private static ICollectionInfo CreateCollection(object?[] values)
   {
      CollectionInfo info = new CollectionInfo(values);
      return info;
   }
   #endregion
}
