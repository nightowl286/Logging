using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.Methods;

namespace TNO.ReadingWriting.Tests.LogData.Methods;

[TestClass]
public class MethodInfoReadWriteTests : BinaryReadWriteTestsBase<MethodInfoSerialiser, MethodInfoDeserialiserLatest, IMethodInfo>
{
   #region Methods
   protected override void Setup(out MethodInfoSerialiser writer, out MethodInfoDeserialiserLatest reader)
   {
      writer = new MethodInfoSerialiser(GeneralSerialiser.Instance);
      reader = new MethodInfoDeserialiserLatest(GeneralDeserialiser.Instance);
   }
   protected override IEnumerable<IMethodInfo> CreateData()
   {
      MethodInfo constructorInfo = new MethodInfo(
         1,
         Array.Empty<IParameterInfo>(),
         "constructor",
         2,
         new ulong[] { 1, 2, 3 });


      yield return constructorInfo;
   }

   protected override void Verify(IMethodInfo expected, IMethodInfo result)
   {
      Assert.That.AreEqual(expected.Name, result.Name);
      Assert.That.AreEqual(expected.DeclaringTypeId, result.DeclaringTypeId);
      Assert.That.AreEqual(expected.ParameterInfos.Count, result.ParameterInfos.Count);
      Assert.That.AreEqual(expected.ReturnTypeId, result.ReturnTypeId);

      Assert.That.AreEqual(expected.GenericTypeIds.Count, result.GenericTypeIds.Count);
      ulong[] expectedGenericTypeIds = (ulong[])expected.GenericTypeIds;
      ulong[] resultGenericTypeIds = (ulong[])result.GenericTypeIds;
      CollectionAssert.AreEqual(expectedGenericTypeIds, resultGenericTypeIds);
   }
   #endregion
}
