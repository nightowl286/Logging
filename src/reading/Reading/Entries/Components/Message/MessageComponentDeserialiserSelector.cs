using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.Components.Message;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.Message.Versions;

namespace TNO.Logging.Reading.Entries.Components.Message;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IMessageComponentDeserialiser"/>.
/// </summary>
internal class MessageComponentDeserialiserSelector : DeserialiserSelectorBase<IMessageComponentDeserialiser>, IMessageComponentDeserialiserSelector
{
   public MessageComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<MessageComponentDeserialiser0>(0);
   }
}
