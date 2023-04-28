using System.Reflection;
using System.Text;
using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.ReadingWriting.Tests;

public abstract class BinaryReadWriteTestsBase<TWriter, TReader, TData> : ReadWriteTestsBase<TWriter, TReader, TData>
   where TWriter : notnull, ISerialiser<TData>
   where TReader : notnull, IDeserialiser<TData>
{
   #region Methods
   protected override void Serialise(TWriter writer, BinaryWriter binaryWriter, TData data) => writer.Serialise(binaryWriter, data);
   protected override TData Deserialise(TReader reader, BinaryReader binaryReader) => reader.Deserialise(binaryReader);
   #endregion
}

[TestCategory(Category.Serialisation)]
public abstract class ReadWriteTestsBase<TWriter, TReader, TData>
   where TWriter : notnull
   where TReader : notnull
{
   #region Properties
   public static Encoding Encoding { get; } = Encoding.UTF8;
   #endregion

   #region Test Methods
   [TestMethod]
   public void Write_Read_Successful()
   {
      foreach (Annotated<TData> annotatedExpected in CreateData())
      {
         try
         {
            // Arrange
            Setup(out TWriter writer, out TReader reader);
            using MemoryStream memoryStream = new MemoryStream();

            // Arrange Assert
            TryCheckVersions(writer, reader);

            // Act
            using (BinaryWriter bw = new BinaryWriter(memoryStream, Encoding, true))
               Serialise(writer, bw, annotatedExpected.Data);

            memoryStream.Position = 0;

            TData result;
            using (BinaryReader br = new BinaryReader(memoryStream, Encoding, true))
               result = Deserialise(reader, br);

            // Assert
            Assert.AreEqual(memoryStream.Length, memoryStream.Position, "Not all written data was read.");
            Verify(annotatedExpected.Data, result);
         }
         catch (Exception ex)
         {
            throw new Exception($"Test failed on data ({annotatedExpected.Annotation}){Environment.NewLine}", ex);
         }
      }
   }
   #endregion

   #region Methods
   protected abstract void Serialise(TWriter writer, BinaryWriter binaryWriter, TData data);
   protected abstract TData Deserialise(TReader reader, BinaryReader binaryReader);
   protected abstract IEnumerable<Annotated<TData>> CreateData();
   private static void TryCheckVersions(TWriter writer, TReader reader)
   {
      Type writerType = writer.GetType();
      Type readerType = reader.GetType();

      bool isVersionedWriter = writerType.IsDefined<VersionAttribute>(false);
      bool isVersionedReader = readerType.IsDefined<VersionAttribute>(false);

      Assert.That.IsInconclusiveIf(isVersionedWriter ^ isVersionedReader, "Mixing versioned and non - versioned readers and writers is not allowed.");

      if (isVersionedWriter && isVersionedReader)
      {
         uint writerVersion = writerType.GetVersion();
         uint readerVersion = readerType.GetVersion();

         Assert.That.IsInconclusiveIf(writerVersion != readerVersion,
            $"There is a mismatch between the reader ({readerVersion}) / writer ({writerVersion}) versions.");
      }
   }

   protected virtual void Setup(out TWriter writer, out TReader reader)
   {
      writer = SetupInstance<TWriter, ISerialiser>(GeneralSerialiser.Instance);
      reader = SetupInstance<TReader, IDeserialiser>(GeneralDeserialiser.Instance);
   }

   private static T SetupInstance<T, U>(U parameter)
   {
      Type type = typeof(T);
      if (type.GetConstructors().Any(c => CanInjectGeneralSerialiser(c, typeof(U))))
      {
         object?[]? parameters = new object?[] { parameter };
         object instance = Activator.CreateInstance(typeof(T), parameters) ?? throw new NullReferenceException($"Setup failed when trying to creates an instance of {typeof(T)}."); ;
         return (T)instance;
      }

      return Activator.CreateInstance<T>();
   }

   private static bool CanInjectGeneralSerialiser(ConstructorInfo constructorInfo, Type targetType)
   {
      ParameterInfo[] parameters = constructorInfo.GetParameters();
      if (parameters.Length == 1)
      {
         ParameterInfo param = parameters[0];
         return param.ParameterType == targetType;
      }

      return false;
   }
   protected abstract void Verify(TData expected, TData result);
   #endregion
}