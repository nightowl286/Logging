using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.LogData;

[TestClass]
public class ContextInfoSerialiserCountTests : BinarySerialiserCountTestBase<ContextInfoSerialiser, ContextInfo>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
      ContextInfo contextInfo = new ContextInfo("context", 5, 5, 5, 5);

      // Act + Assert
      CountTestBase(contextInfo);
   }
   #endregion
}
