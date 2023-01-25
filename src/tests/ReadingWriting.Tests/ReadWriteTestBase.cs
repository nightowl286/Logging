using System.Text;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.ReadingWriting.Tests;

public abstract class ReadWriteTestBase<TWriter, TReader, TData>
   where TWriter : ISerialiser<TData>
   where TReader : IDeserialiser<TData>
{
   #region Properties
   public static Encoding Encoding { get; } = Encoding.UTF8;
   #endregion

   #region Test Methods
   [TestMethod]
   public void Write_Read_Successful()
   {
      // Arrange
      TData expected = CreateData();
      Setup(out TWriter writer, out TReader reader);
      using MemoryStream memoryStream = new MemoryStream();

      // Act
      using (BinaryWriter bw = new BinaryWriter(memoryStream, Encoding, true))
         writer.Serialise(bw, expected);

      memoryStream.Position = 0;

      TData result;
      using (BinaryReader br = new BinaryReader(memoryStream, Encoding, true))
         result = reader.Deserialise(br);

      // Assert
      Assert.AreEqual(memoryStream.Length, memoryStream.Position, "Not all written data was read.");
      Verify(expected, result);
   }
   #endregion

   #region Methods
   [TestInitialize]
   protected virtual void Setup(out TWriter writer, out TReader reader)
   {
      writer = Activator.CreateInstance<TWriter>();
      reader = Activator.CreateInstance<TReader>();
   }
   protected abstract TData CreateData();
   protected abstract void Verify(TData expected, TData result);
   #endregion
}
