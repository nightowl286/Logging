using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.Constructors;
using TNO.Logging.Writing.Serialisers.LogData.Methods;

namespace TNO.ReadingWriting.Tests.LogData.Methods;

[TestClass]
public class ConstructorInfoReadWriteTests : ReadWriteTestsBase<ConstructorInfoSerialiser, ConstructorInfoDeserialiserLatest, IConstructorInfo>
{
   #region Methods
   protected override void Setup(out ConstructorInfoSerialiser writer, out ConstructorInfoDeserialiserLatest reader)
   {
      writer = new ConstructorInfoSerialiser(
         new ParameterInfoSerialiser());

      reader = new ConstructorInfoDeserialiserLatest(
         new ParameterInfoDeserialiserLatest());
   }
   protected override IConstructorInfo CreateData()
   {
      ConstructorInfo constructorInfo = new ConstructorInfo(
         1,
         Array.Empty<IParameterInfo>(),
         "constructor");


      return constructorInfo;
   }

   protected override void Verify(IConstructorInfo expected, IConstructorInfo result)
   {
      Assert.That.AreEqual(expected.Name, result.Name);
      Assert.That.AreEqual(expected.DeclaringTypeId, result.DeclaringTypeId);
      Assert.That.AreEqual(expected.ParameterInfos.Count, result.ParameterInfos.Count);
   }
   #endregion
}
