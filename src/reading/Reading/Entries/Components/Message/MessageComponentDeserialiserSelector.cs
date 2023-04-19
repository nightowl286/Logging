using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.Message.Versions;

namespace TNO.Logging.Reading.Entries.Components.Message;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IMessageComponent"/>.
/// </summary>
internal class MessageComponentDeserialiserSelector : DeserialiserSelectorBase<IMessageComponent>
{
   public MessageComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<MessageComponentDeserialiser0>(0);
   }
}