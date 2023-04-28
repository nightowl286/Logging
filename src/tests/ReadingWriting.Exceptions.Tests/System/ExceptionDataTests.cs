using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Exceptions.System;
using TNO.Logging.Common.Exceptions.TestExtensions;
using TNO.Logging.Reading.Exceptions.System;
using TNO.Logging.Writing.Exceptions.System;

namespace TNO.ReadingWriting.Exceptions.Tests.System;

[TestClass]
public class ExceptionDataTests : ExceptionDataTestBase<Exception, IExceptionData>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      ExceptionData data = ExceptionData.Empty;
      ExceptionHandler handler = new ExceptionHandler();

      // Act + Assert
      CheckCount(handler, data);
   }

   [TestMethod]
   public void ReadWrite()
   {
      // Arrange
      ExceptionData data = ExceptionData.Empty;
      ExceptionHandler handler = new ExceptionHandler();
      ExceptionDataDeserialiser deserialiser = new ExceptionDataDeserialiser();

      // Act + Assert
      CheckReadWrite(handler, deserialiser, data);
   }
   #endregion

   #region Methods
   protected override void Verify(IExceptionData expected, IExceptionData result) { }
   #endregion
}
