using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Writing.Serialisers.LogData.General;
using TNO.Writing.Tests.BinarySerialiserCountTests;

namespace TNO.Writing.Tests.CountTests.General;

[TestClass]
public class CollectionInfoSerialiserCountTests : BinarySerialiserCountTestBase<CollectionInfoSerialiser, ICollectionInfo>
{
   #region Methods
   protected override CollectionInfoSerialiser Setup() => (CollectionInfoSerialiser)GeneralSerialiser.Instance.Get<ICollectionInfo>();

   [DynamicData(nameof(ValidPrimitiveValues.AsArguments), typeof(ValidPrimitiveValues), DynamicDataSourceType.Method)]
   [TestMethod]
   public void Count_WithOne(object? value)
   {
      // Arrange
      List<object?> collection = new List<object?>() { value };

      CollectionInfo collectionInfo = new CollectionInfo(collection);

      // Act + Assert
      CountTestBase(collectionInfo);
   }

   [TestMethod]
   public void Count_WithAll()
   {
      // Arrange
      List<object?> collection = ValidPrimitiveValues.Values.ToList();

      CollectionInfo collectionInfo = new CollectionInfo(collection);

      // Act + Assert
      CountTestBase(collectionInfo);
   }
   #endregion
}
