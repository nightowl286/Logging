using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Message;

namespace TNO.Logging.Reading.Entries.Components.Message.Versions;

internal sealed class MessageComponentDeserialiser0 : IMessageComponentDeserialiser
{
   #region Methods
   public IMessageComponent Deserialise(BinaryReader reader)
   {
      string message = reader.ReadString();

      return MessageComponentFactory.Version0(message);
   }
   #endregion
}
