using TNO.Logging.Common.Exceptions.Abstractions.System;
using TNO.Logging.Common.Exceptions.System;
using TNO.Logging.Common.Exceptions.TestExtensions;
using TNO.Logging.Reading.Exceptions.System;
using TNO.Logging.Writing.Exceptions.System;
using TNO.Tests.Common;

namespace TNO.ReadingWriting.Exceptions.Tests.System;

[TestClass]
public class ArgumentExceptionDataTests : ExceptionDataTestBase<ArgumentException, IArgumentExceptionData>
{
   #region Tests
   [DataRow(null, DisplayName = "null")]
   [DataRow("", DisplayName = "empty")]
   [DataRow("parameter")]
   [TestMethod]
   public void Count(string? parameterName)
   {
      // Arrange
      ArgumentExceptionData data = new ArgumentExceptionData(parameterName);
      ArgumentExceptionHandler handler = new ArgumentExceptionHandler();

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
      ArgumentExceptionData data = new ArgumentExceptionData(parameterName);
      ArgumentExceptionHandler handler = new ArgumentExceptionHandler();
      ArgumentExceptionDataDeserialiser deserialiser = new ArgumentExceptionDataDeserialiser();

      // Act + Assert
      CheckReadWrite(handler, deserialiser, data);
   }
   #endregion

   #region Methods
   protected override void Verify(IArgumentExceptionData expected, IArgumentExceptionData result)
   {
      Assert.That.AreEqual(expected.ParameterName, result.ParameterName);
   }
   #endregion
}
