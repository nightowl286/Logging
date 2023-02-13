using System.Reflection;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Tests.Common;

namespace Common.Abstractions.Tests.entries;

[TestClass]
[TestCategory(Category.Purpose)]
public sealed class PurposeTests
{
   #region Tests
   [TestMethod]
   public void GetAll_CountMatchesPropertyCount()
   {
      // Arrange
      int expected = typeof(Purpose)
         .GetProperties(BindingFlags.Public | BindingFlags.Static)
         .Length;

      // Act
      Importance[] values = Purpose.GetAll().ToArray();

      // Assert
      Assert.That.IsInconclusiveIf(values.Length != expected,
         "It is likely that this result has cascaded, please check the other tests (and code tests) first.");

   }
   #endregion
}
