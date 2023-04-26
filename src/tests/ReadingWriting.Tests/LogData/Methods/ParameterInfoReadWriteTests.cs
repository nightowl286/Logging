using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.Methods;

namespace TNO.ReadingWriting.Tests.LogData.Methods;

[TestClass]
public class ParameterInfoReadWriteTests : BinaryReadWriteTestsBase<ParameterInfoSerialiser, ParameterInfoDeserialiserLatest, IParameterInfo>
{
   #region Methods
   protected override IEnumerable<IParameterInfo> CreateData()
   {
      ParameterInfo parameterInfo = new ParameterInfo(
        1,
        ParameterModifier.Params,
        true,
        "parameter");


      yield return parameterInfo;
   }

   protected override void Verify(IParameterInfo expected, IParameterInfo result)
   {
      Assert.That.AreEqual(expected.Name, result.Name);
      Assert.That.AreEqual(expected.Modifier, result.Modifier);
      Assert.That.AreEqual(expected.HasDefaultValue, result.HasDefaultValue);
      Assert.That.AreEqual(expected.TypeId, result.TypeId);
   }
   #endregion
}
