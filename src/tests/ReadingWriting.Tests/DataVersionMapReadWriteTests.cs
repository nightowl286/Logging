using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Reading;
using TNO.Logging.Writing.Serialisers;

namespace TNO.ReadingWriting.Tests;

[TestClass]
public class DataVersionMapReadWriteTests : BinaryReadWriteTestsBase<DataVersionMapSerialiser, DataVersionMapDeserialiser, DataVersionMap>
{
   #region Methods
   protected override IEnumerable<Annotated<DataVersionMap>> CreateData()
   {
      DataVersionMap map = new DataVersionMap
      {
         new DataKindVersion(VersionedDataKind.Entry, 1)
      };

      yield return new(map);
   }
   protected override void Verify(DataVersionMap expected, DataVersionMap result)
   {
      Assert.That.AreEqual(expected.Count, result.Count, "The result contains a different amount of versions.");

      foreach (DataKindVersion dataKindVersion in expected)
      {
         bool contains = result.TryGetVersion(dataKindVersion.DataKind, out uint resultVersion);
         Assert.IsTrue(contains, $"Result does not contain the kind ({dataKindVersion.DataKind}).");

         Assert.That.AreEqual(resultVersion, resultVersion, $"The version for the kind ({dataKindVersion.DataKind}) does not match.");
      }
   }
   #endregion
}