using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.LogData;
using TNO.Logging.Writing.Serialisers.LogData.Types;

namespace TNO.ReadingWriting.Tests.LogData.Types;

[TestClass]
public class TypeReferenceReadWriteTests : BinaryReadWriteTestsBase<TypeReferenceSerialiser, TypeReferenceDeserialiserLatest, TypeReference>
{
   #region Methods
   protected override void Setup(out TypeReferenceSerialiser writer, out TypeReferenceDeserialiserLatest reader)
   {
      writer = new TypeReferenceSerialiser(GeneralSerialiser.Instance);
      reader = new TypeReferenceDeserialiserLatest(GeneralDeserialiser.Instance);
   }

   protected override IEnumerable<TypeReference> CreateData()
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

      TypeReference typeReference = new TypeReference(typeInfo, 1);

      yield return typeReference;
   }

   protected override void Verify(TypeReference expected, TypeReference result)
   {
      Assert.That.AreEqual(expected.Id, result.Id);

      ITypeInfo expectedInfo = expected.TypeInfo;
      ITypeInfo resultInfo = expected.TypeInfo;

      Assert.That.AreEqual(expectedInfo.AssemblyId, resultInfo.AssemblyId);
      Assert.That.AreEqual(expectedInfo.BaseTypeId, resultInfo.BaseTypeId);
      Assert.That.AreEqual(expectedInfo.DeclaringTypeId, resultInfo.DeclaringTypeId);
      Assert.That.AreEqual(expectedInfo.Name, resultInfo.Name);
      Assert.That.AreEqual(expectedInfo.FullName, resultInfo.FullName);
      Assert.That.AreEqual(expectedInfo.Namespace, resultInfo.Namespace);

      List<ulong> expectedInfoGenericTypeIds = expectedInfo.GenericTypeIds.ToList();
      List<ulong> resultInfoGenericTypeIds = resultInfo.GenericTypeIds.ToList();
      CollectionAssert.AreEqual(expectedInfoGenericTypeIds, resultInfoGenericTypeIds);
   }
   #endregion
}
