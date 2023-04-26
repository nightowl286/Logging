using TNO.Logging.Writing.Serialisers.LogData.General;
using TNO.Writing.Tests.BinarySerialiserCountTests;

namespace TNO.Writing.Tests.CountTests.General;

[TestClass]
public class PrimitiveSerialiserCountTests : SerialiserCountTestBase<PrimitiveSerialiser, object?>
{
   #region Methods
   [DynamicData(nameof(ValidPrimitiveValues.AsArguments), typeof(ValidPrimitiveValues), DynamicDataSourceType.Method)]
   [TestMethod]
   public void Count(object? value) => CountTestBase(value);

   protected override int Count(PrimitiveSerialiser writer, object? data) => writer.Count(data);
   protected override void Serialise(PrimitiveSerialiser writer, BinaryWriter binaryWriter, object? data) => writer.Serialise(binaryWriter, data);
   #endregion
}
