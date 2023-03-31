using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.LogData;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData;

[TestClass]
public class TypeInfoSerialiserCountTests : BinarySerialiserCountTestBase<TypeInfoSerialiser, ITypeInfo>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      TypeInfo typeInfo = new TypeInfo(
         0,
         1,
         3,
         2,
         "name",
         "full name",
         "name space",
         new List<ulong> { 1, 2, 3 });

      // Act + Verify
      CountTestBase(typeInfo);
   }
   #endregion
}
