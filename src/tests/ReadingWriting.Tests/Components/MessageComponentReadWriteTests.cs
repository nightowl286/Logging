using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class MessageComponentReadWriteTests : BinaryReadWriteTestsBase<MessageComponentSerialiser, MessageComponentDeserialiserLatest, IMessageComponent>
{
   #region Methods
   protected override IEnumerable<IMessageComponent> CreateData()
   {
      string message = "message";
      MessageComponent component = new MessageComponent(message);

      yield return component;
   }
   protected override void Verify(IMessageComponent expected, IMessageComponent result)
   {
      Assert.That.AreEqual(expected.Message, result.Message);
   }
   #endregion
}