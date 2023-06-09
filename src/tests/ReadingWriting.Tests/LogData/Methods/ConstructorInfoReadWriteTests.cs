﻿using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.Methods;

namespace TNO.ReadingWriting.Tests.LogData.Methods;

[TestClass]
public class ConstructorInfoReadWriteTests : BinaryReadWriteTestsBase<ConstructorInfoSerialiser, ConstructorInfoDeserialiserLatest, IConstructorInfo>
{
   #region Methods
   protected override void Setup(out ConstructorInfoSerialiser writer, out ConstructorInfoDeserialiserLatest reader)
   {
      writer = new ConstructorInfoSerialiser(GeneralSerialiser.Instance);
      reader = new ConstructorInfoDeserialiserLatest(GeneralDeserialiser.Instance);
   }
   protected override IEnumerable<Annotated<IConstructorInfo>> CreateData()
   {
      ConstructorInfo constructorInfo = new ConstructorInfo(
         1,
         Array.Empty<IParameterInfo>(),
         "constructor");

      yield return new(constructorInfo);
   }

   protected override void Verify(IConstructorInfo expected, IConstructorInfo result)
   {
      Assert.That.AreEqual(expected.Name, result.Name);
      Assert.That.AreEqual(expected.DeclaringTypeId, result.DeclaringTypeId);
      Assert.That.AreEqual(expected.ParameterInfos.Count, result.ParameterInfos.Count);
   }
   #endregion
}
