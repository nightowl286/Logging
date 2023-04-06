using System.Text;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.ReadingWriting.Tests;

[TestCategory(Category.Serialisation)]
public abstract class ReadWriteTestBase<TWriter, TReader, TData>
   where TWriter : IBinarySerialiser<TData>
   where TReader : IBinaryDeserialiser<TData>
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

      // Arrange Assert
      TryCheckVersions(writer, reader);

      // Act
      using (BinaryWriter bw = new BinaryWriter(memoryStream, Encoding, true))
         writer.Serialise(bw, expected);

      memoryStream.Position = 0;

      TData result;
      using (BinaryReader br = new BinaryReader(memoryStream, Encoding, true))
         result = reader.Deserialise(br);

      // Assert
      Assert.That.AreEqual(memoryStream.Length, memoryStream.Position, "Not all written data was read.");
      Verify(expected, result);
   }
   #endregion

   #region Methods
   private static void TryCheckVersions(TWriter writer, TReader reader)
   {
      IVersioned? versionedWriter = writer as IVersioned;
      IVersioned? versionedReader = reader as IVersioned;

      Assert.That.IsInconclusiveIf(versionedWriter is null ^ versionedReader is null, "Mixing versioned and non-versioned readers and writers is not allowed.");

      if (versionedWriter is not null && versionedReader is not null)
      {
         Assert.That.IsInconclusiveIf(versionedWriter.Version != versionedReader.Version,
            $"There is a mismatch between the reader ({versionedReader.Version}) / writer ({versionedWriter.Version}) versions.");
      }
   }

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