using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.LogData;
using TNO.Logging.Writing.Serialisers.LogData.Types;

namespace TNO.ReadingWriting.Tests.LogData.Types;

[TestClass]
public class TypeInfoReadWriteTests : ReadWriteTestsBase<TypeInfoSerialiser, TypeInfoDeserialiserLatest, ITypeInfo>
{
   #region Methods
   protected override ITypeInfo CreateData()
   {
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

      return typeInfo;
   }

   protected override void Verify(ITypeInfo expected, ITypeInfo result)
   {
      Assert.That.AreEqual(expected.AssemblyId, result.AssemblyId);
      Assert.That.AreEqual(expected.BaseTypeId, result.BaseTypeId);
      Assert.That.AreEqual(expected.DeclaringTypeId, result.DeclaringTypeId);
      Assert.That.AreEqual(expected.ElementTypeId, result.ElementTypeId);
      Assert.That.AreEqual(expected.Name, result.Name);
      Assert.That.AreEqual(expected.FullName, result.FullName);
      Assert.That.AreEqual(expected.Namespace, result.Namespace);

      List<ulong> expectedGenericTypeIds = expected.GenericTypeIds.ToList();
      List<ulong> resultGenericTypeIds = result.GenericTypeIds.ToList();
      CollectionAssert.AreEqual(expectedGenericTypeIds, resultGenericTypeIds);
   }
   #endregion
}
