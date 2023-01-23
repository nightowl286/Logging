using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Entries.Components;

internal sealed class MessageComponentSerialiser : IMessageComponentSerialiser
{
   #region Methods
   public void Serialise(BinaryWriter writer, IMessageComponent data)
   {
      string message = data.Message;
      writer.Write(message);
   }
   #endregion
}
