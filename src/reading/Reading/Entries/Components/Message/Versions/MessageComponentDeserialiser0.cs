using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Message;

namespace TNO.Logging.Reading.Entries.Components.Message.Versions;

/// <summary>
/// A deserialiser for <see cref="IMessageComponent"/>, version #0.
/// </summary>
public sealed class MessageComponentDeserialiser0 : IMessageComponentDeserialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IMessageComponent Deserialise(BinaryReader reader)
   {
      string message = reader.ReadString();

      return MessageComponentFactory.Version0(message);
   }
   #endregion
}
