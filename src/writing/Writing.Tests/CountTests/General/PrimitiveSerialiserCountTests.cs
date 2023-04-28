using TNO.Logging.Writing.Serialisers.LogData.General;
using TNO.Writing.Tests.BinarySerialiserCountTests;

namespace TNO.Writing.Tests.CountTests.General;

[TestClass]
public class PrimitiveSerialiserCountTests : SerialiserCountTestBase<PrimitiveSerialiser, object?>
{
   #region Methods
   [DynamicData(
      nameof(ValidPrimitiveValues.AsArguments),
      typeof(ValidPrimitiveValues),
      DynamicDataSourceType.Method,
      DynamicDataDisplayName = nameof(ValidPrimitiveValues.GetDisplayName),
      DynamicDataDisplayNameDeclaringType = typeof(ValidPrimitiveValues))]
   [TestMethod]
   public void Count(Annotated annotated) => CountTestBase(annotated.Data);

   protected override int Count(PrimitiveSerialiser writer, object? data) => writer.Count(data);
   protected override void Serialise(PrimitiveSerialiser writer, BinaryWriter binaryWriter, object? data) => writer.Serialise(binaryWriter, data);
   #endregion
}
