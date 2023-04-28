using TNO.Logging.Reading.LogData.General;
using TNO.Logging.Writing.Serialisers.LogData.General;

namespace TNO.ReadingWriting.Tests.LogData.General;

[TestClass]
public class PrimitiveReadWriteTests : ReadWriteTestsBase<PrimitiveSerialiser, PrimitiveDeserialiser, object?>
{
   #region Methods
   protected override IEnumerable<Annotated<object?>> CreateData() => ValidPrimitiveValues.Values;
   protected override object? Deserialise(PrimitiveDeserialiser reader, BinaryReader binaryReader) => reader.Deserialise(binaryReader);
   protected override void Serialise(PrimitiveSerialiser writer, BinaryWriter binaryWriter, object? data) => writer.Serialise(binaryWriter, data);
   protected override void Verify(object? expected, object? result) => Assert.That.AreEqual(expected, result);
   #endregion
}
