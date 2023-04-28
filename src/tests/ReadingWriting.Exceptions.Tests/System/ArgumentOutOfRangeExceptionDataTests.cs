using Moq;
using TNO.Logging.Common.Exceptions.Abstractions.System;
using TNO.Logging.Common.Exceptions.System;
using TNO.Logging.Common.Exceptions.TestExtensions;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Exceptions.System;
using TNO.Logging.Reading.LogData.General;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Exceptions.System;
using TNO.Logging.Writing.Serialisers.LogData.General;
using TNO.Tests.Common;

namespace TNO.ReadingWriting.Exceptions.Tests.System;

[TestClass]
public class ArgumentOutOfRangeExceptionDataTests : ExceptionDataTestBase<ArgumentOutOfRangeException, IArgumentOutOfRangeExceptionData>
{
   #region Tests
   [DataRow(null, DisplayName = "null")]
   [DataRow("", DisplayName = "empty")]
   [DataRow("parameter")]
   [TestMethod]
   public void Count(string? parameterName)
   {
      // Arrange
      ArgumentOutOfRangeExceptionData data = new ArgumentOutOfRangeExceptionData(parameterName, 5);
      ArgumentOutOfRangeExceptionHandler handler = CreateHandler();

      // Act + Assert
      CheckCount(handler, data);
   }

   [DataRow(null, DisplayName = "null")]
   [DataRow("", DisplayName = "empty")]
   [DataRow("parameter")]
   [TestMethod]
   public void ReadWrite(string? parameterName)
   {
      // Arrange
      ArgumentOutOfRangeExceptionData data = new ArgumentOutOfRangeExceptionData(parameterName, 5);
      ArgumentOutOfRangeExceptionHandler handler = CreateHandler();
      ArgumentOutOfRangeExceptionDataDeserialiser deserialiser = new ArgumentOutOfRangeExceptionDataDeserialiser(
         new PrimitiveDeserialiser(
            Mock.Of<IDeserialiser>()));

      // Act + Assert
      CheckReadWrite(handler, deserialiser, data);
   }
   #endregion

   #region Methods
   private static ArgumentOutOfRangeExceptionHandler CreateHandler()
   {
      return new ArgumentOutOfRangeExceptionHandler(
         Mock.Of<ILogWriteContext>(),
         Mock.Of<ILogDataCollector>(),
         new PrimitiveSerialiser(
            Mock.Of<ISerialiser>()));
   }
   protected override void Verify(IArgumentOutOfRangeExceptionData expected, IArgumentOutOfRangeExceptionData result)
   {
      Assert.That.AreEqual(expected.ParameterName, result.ParameterName);
      Assert.That.AreEqual(expected.ActualValue, result.ActualValue);
   }
   #endregion
}
