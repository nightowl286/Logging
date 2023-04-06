using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.LogData;
using TNO.Logging.Writing.Serialisers.LogData.Types;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData.Types;

[TestClass]
public class TypeInfoSerialiserCountTests : BinarySerialiserCountTestBase<TypeInfoSerialiser, ITypeInfo>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      TypeInfo typeInfo = new TypeInfo(
         2,
         3,
         4,
         "name",
         "full name",
         "name space",
         new List<ulong> { 1, 2, 3 });

      // Act + Verify
      CountTestBase(typeInfo);
   }
   #endregion
}
