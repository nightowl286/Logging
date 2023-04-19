using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Entries.Components.Message.Versions;

/// <summary>
/// A deserialiser for <see cref="IMessageComponent"/>, version #0.
/// </summary>
[Version(0)]
public sealed class MessageComponentDeserialiser0 : IDeserialiser<IMessageComponent>
{
   #region Methods
   /// <inheritdoc/>
   public IMessageComponent Deserialise(BinaryReader reader)
   {
      string message = reader.ReadString();

      return MessageComponentFactory.Version0(message);
   }
   #endregion
}