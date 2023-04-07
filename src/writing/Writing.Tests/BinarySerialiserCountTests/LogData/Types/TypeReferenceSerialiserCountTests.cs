using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.LogData;
using TNO.Logging.Writing.Serialisers.LogData.Types;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData.Types;

[TestClass]
public class TypeReferenceSerialiserCountTests : BinarySerialiserCountTestBase<TypeReferenceSerialiser, TypeReference>
{
   #region Methods
   protected override TypeReferenceSerialiser Setup()
   {
      return new TypeReferenceSerialiser(
         new TypeInfoSerialiser());
   }
   #endregion

   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      TypeInfo typeInfo = new TypeInfo(
         2,
         3,
         4,
         5,
         6,
         "name",
         "full name",
         "name space",
         new List<ulong> { 1, 2, 3 });

      TypeReference reference = new TypeReference(typeInfo, 1);

      // Act + Verify
      CountTestBase(reference);
   }
   #endregion
}
