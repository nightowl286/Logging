using System.Text;
using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.ReadingWriting.Tests;

[TestCategory(Category.Serialisation)]
public abstract class ReadWriteTestsBase<TWriter, TReader, TData>
   where TWriter : ISerialiser<TData>
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
      Type writerType = writer.GetType();
      Type readerType = reader.GetType();

      bool isVersionedWriter = writerType.IsDefined<VersionAttribute>(false);
      bool isVersionedReader = readerType.IsDefined<VersionAttribute>(false);

      Assert.That.IsInconclusiveIf(isVersionedWriter ^ isVersionedReader, "Mixing versioned and non-versioned readers and writers is not allowed.");

      if (isVersionedWriter && isVersionedReader)
      {
         uint writerVersion = writerType.GetVersion();
         uint readerVersion = readerType.GetVersion();

         Assert.That.IsInconclusiveIf(writerVersion != readerVersion,
            $"There is a mismatch between the reader ({readerVersion}) / writer ({writerVersion}) versions.");
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