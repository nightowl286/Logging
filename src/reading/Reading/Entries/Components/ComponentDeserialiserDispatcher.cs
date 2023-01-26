using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Message;

namespace TNO.Logging.Reading.Entries.Components;
internal class ComponentDeserialiserDispatcher : IComponentDeserialiserDispatcher
{
   #region Fields
   private readonly IMessageComponentDeserialiser _messageDeserialiser;
   #endregion
   public ComponentDeserialiserDispatcher(IMessageComponentDeserialiser messageDeserialiser)
   {
      _messageDeserialiser = messageDeserialiser;
   }

   #region Methods
   public IComponent Deserialise(BinaryReader reader, ComponentKind componentKind)
   {
      if (componentKind is ComponentKind.Message)
         return _messageDeserialiser.Deserialise(reader);

      throw new ArgumentException($"Unknown component kind ({componentKind}).");
   }
   #endregion
}
