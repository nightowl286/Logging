using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.Components.Message;
using TNO.Logging.Reading.Deserialisers;

namespace TNO.Logging.Reading.Entries.Components.Message;

/// <summary>
/// An <see cref="IDeserialiserSelector{T, U}"/> for versions of the <see cref="IMessageComponentDeserialiser"/>.
/// </summary>
internal class MessageComponentDeserialiserSelector : DeserialiserSelectorBase<IMessageComponentDeserialiser, IMessageComponent>
{
   public MessageComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<MessageComponentDeserialiser0>(0);
   }
}
