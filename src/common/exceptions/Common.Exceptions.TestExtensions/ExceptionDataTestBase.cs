using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Tests.Common;

namespace TNO.Logging.Common.Exceptions.TestExtensions;

/// <summary>
/// A base class for <see cref="IExceptionData"/> tests.
/// </summary>
/// <typeparam name="TException">The type of the <see cref="Exception"/>.</typeparam>
/// <typeparam name="TData">The type of the <see cref="IExceptionData"/>.</typeparam>
public abstract class ExceptionDataTestBase<TException, TData>
   where TException : notnull, Exception
   where TData : notnull, IExceptionData
{
   #region Properties
   /// <summary>The encoding that will be used to save the data.</summary>
   public static Encoding Encoding { get; } = Encoding.UTF8;
   #endregion

   #region Methods
   /// <summary>Checks whether the data is correctly serialised and deserialised.</summary>
   /// <param name="serialiser">The serialise to use.</param>
   /// <param name="deserialiser">The deserialiser to use.</param>
   /// <param name="data">The expected data to serialise.</param>
   /// <remarks>This method will use <see cref="Verify(TData, TData)"/> to ensure that the read data matches the expected data.</remarks>
   protected void CheckReadWrite(ISerialiser<TData> serialiser, IDeserialiser<TData> deserialiser, TData data)
   {
      // Arrange
      using MemoryStream memoryStream = new MemoryStream();

      // Act
      using (BinaryWriter bw = new BinaryWriter(memoryStream, Encoding.UTF8, true))
         serialiser.Serialise(bw, data);

      memoryStream.Position = 0;

      TData result;
      using (BinaryReader reader = new BinaryReader(memoryStream, Encoding, true))
         result = deserialiser.Deserialise(reader);

      // Assert
      Assert.AreEqual(memoryStream.Length, memoryStream.Position, "The deserialiser didn't read all of the data.");
      Verify(data, result);
   }

   /// <summary>Checks that the <see cref="ISerialiser{T}.Count(T)"/> method matches the actual serialised size.</summary>
   /// <param name="serialiser">The serialiser to use.</param>
   /// <param name="data">The data to count and serialise.</param>
   protected void CheckCount(ISerialiser<TData> serialiser, TData data)
   {
      // Arrange
      int expectedCount = GetActualSize(serialiser, data);

      // Act
      int actualCount = serialiser.Count(data);

      // Assert
      Assert.That.AreEqual(expectedCount, actualCount);
   }

   /// <summary>Verifies that the <paramref name="expected"/> data matches the <paramref name="result"/> result.</summary>
   /// <param name="expected">The expected data.</param>
   /// <param name="result">The result data.</param>
   protected abstract void Verify(TData expected, TData result);
   #endregion

   #region Helpers
   private static int GetActualSize(ISerialiser<TData> serialiser, TData data)
   {
      using (MemoryStream memoryStream = new MemoryStream())
      {
         using (BinaryWriter bw = new BinaryWriter(memoryStream, Encoding, true))
            serialiser.Serialise(bw, data);

         return (int)memoryStream.Length;
      }
   }
   #endregion
}
