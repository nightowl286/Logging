using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.LogData;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.ReadingWriting.Tests.LogData;

[TestClass]
public class TypeInfoReadWriteTest : ReadWriteTestBase<TypeInfoSerialiser, TypeInfoDeserialiserLatest, ITypeInfo>
{
   #region Methods
   protected override ITypeInfo CreateData()
   {
      TypeInfo typeInfo = new TypeInfo(
         0,
         1,
         3,
         2,
         "name",
         "full name",
         "name space",
         new List<ulong> { 1, 2, 3 });

      return typeInfo;
   }

   protected override void Verify(ITypeInfo expected, ITypeInfo result)
   {
      Assert.That.AreEqual(expected.Id, result.Id);
      Assert.That.AreEqual(expected.AssemblyId, result.AssemblyId);
      Assert.That.AreEqual(expected.BaseTypeId, result.BaseTypeId);
      Assert.That.AreEqual(expected.DeclaringTypeId, result.DeclaringTypeId);
      Assert.That.AreEqual(expected.Name, result.Name);
      Assert.That.AreEqual(expected.FullName, result.FullName);
      Assert.That.AreEqual(expected.Namespace, result.Namespace);

      List<ulong> expectedGenericTypeIds = expected.GenericTypeIds.ToList();
      List<ulong> resultGenericTypeIds = result.GenericTypeIds.ToList();
      CollectionAssert.AreEqual(expectedGenericTypeIds, resultGenericTypeIds);
   }
   #endregion
}
