using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Reading.Entries.Components.Message.Versions;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
public class MessageComponentReadWriteTest : ReadWriteTestBase<MessageComponentSerialiser, MessageComponentDeserialiser0, IMessageComponent>
{
   #region Methods
   protected override IMessageComponent CreateData()
   {
      string message = "message";
      MessageComponent component = new MessageComponent(message);

      return component;
   }
   protected override void Verify(IMessageComponent expected, IMessageComponent result)
   {
      Assert.AreEqual(expected.Message, result.Message);
   }
   #endregion
}
