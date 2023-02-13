using System.Reflection;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Tests.Common;

namespace Common.Abstractions.Tests.entries;

[TestClass]
[TestCategory(Category.Severity)]
public sealed class SeverityTests
{
   #region Tests
   [TestMethod]
   public void GetAll_CountMatchesPropertyCount()
   {
      // Arrange
      int expected = typeof(Severity)
         .GetProperties(BindingFlags.Public | BindingFlags.Static)
         .Length;

      // Act
      Importance[] values = Severity.GetAll().ToArray();

      // Assert
      Assert.That.IsInconclusiveIf(values.Length != expected,
         "It is likely that this result has cascaded, please check the other tests (and code tests) first.");

   }
   #endregion
}