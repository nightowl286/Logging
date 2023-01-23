using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Message;

namespace TNO.Logging.Reading.Entries.Components.Message;

internal sealed class MessageComponentDeserialiser0 : IMessageComponentDeserialiser
{
   #region Methods
   public IMessageComponent Deserialise(BinaryReader reader)
   {
      string message = reader.ReadString();
      MessageComponent component = new MessageComponent(message);

      return component;
   }
   #endregion
}
