using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.Writing.Tests.BinarySerialiserCountTests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class MessageComponentSerialiserCountTests : BinarySerialiserCountTestBase<MessageComponentSerialiser, IMessageComponent>
{
   #region Tests
   [DataRow("Some data", DisplayName = "short data")]
   [DataRow("", DisplayName = "Empty")]
   [DataRow(data1: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
      DisplayName = "Over 255 in length")]
   [TestMethod]
   public void Count(string message)
   {
      // Arrange
      MessageComponent component = new MessageComponent(message);

      // Act + Assert
      CountTestBase(component);
   }
   #endregion
}