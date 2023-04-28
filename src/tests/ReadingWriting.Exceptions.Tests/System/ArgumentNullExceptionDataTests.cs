using TNO.Logging.Common.Exceptions.Abstractions.System;
using TNO.Logging.Common.Exceptions.System;
using TNO.Logging.Common.Exceptions.TestExtensions;
using TNO.Logging.Reading.Exceptions.System;
using TNO.Logging.Writing.Exceptions.System;
using TNO.Tests.Common;

namespace TNO.ReadingWriting.Exceptions.Tests.System;

[TestClass]
public class ArgumentNullExceptionDataTests : ExceptionDataTestBase<ArgumentNullException, IArgumentNullExceptionData>
{
   #region Tests
   [DataRow(null, DisplayName = "null")]
   [DataRow("", DisplayName = "empty")]
   [DataRow("parameter")]
   [TestMethod]
   public void Count(string? parameterName)
   {
      // Arrange
      ArgumentNullExceptionData data = new ArgumentNullExceptionData(parameterName);
      ArgumentNullExceptionHandler handler = new ArgumentNullExceptionHandler();

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
      ArgumentNullExceptionData data = new ArgumentNullExceptionData(parameterName);
      ArgumentNullExceptionHandler handler = new ArgumentNullExceptionHandler();
      ArgumentNullExceptionDataDeserialiser deserialiser = new ArgumentNullExceptionDataDeserialiser();

      // Act + Assert
      CheckReadWrite(handler, deserialiser, data);
   }
   #endregion

   #region Methods
   protected override void Verify(IArgumentNullExceptionData expected, IArgumentNullExceptionData result)
   {
      Assert.That.AreEqual(expected.ParameterName, result.ParameterName);
   }
   #endregion
}
