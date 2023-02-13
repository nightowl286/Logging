using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Reading;
using TNO.Logging.Writing;

namespace TNO.ReadingWriting.Tests;

[TestClass]
public class DataVersionMapReadWriteTest : ReadWriteTestBase<DataVersionMapSerialiser, DataVersionMapDeserialiser, DataVersionMap>
{
   #region Methods
   protected override DataVersionMap CreateData()
   {
      DataVersionMap map = new DataVersionMap
      {
         { VersionedDataKind.Entry, 1 }
      };

      return map;
   }
   protected override void Verify(DataVersionMap expected, DataVersionMap result)
   {
      Assert.AreEqual(expected.Count, result.Count, "The result contains a different amount of versions.");

      foreach (KeyValuePair<VersionedDataKind, uint> pair in expected)
      {
         bool contains = result.TryGetValue(pair.Key, out uint resultValue);
         Assert.IsTrue(contains, $"Result does not contain the kind ({pair.Key}).");

         Assert.AreEqual(pair.Value, resultValue, $"The version for the kind ({pair.Key}) does not match.");
      }
   }
   #endregion
}