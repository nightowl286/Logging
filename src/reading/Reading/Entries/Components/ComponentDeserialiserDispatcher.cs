using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Message;

namespace TNO.Logging.Reading.Entries.Components;

/// <summary>
/// Denotes a <see cref="IDeserialiser{T}"/> dispatcher that 
/// will deserialise a <see cref="IComponent"/> based on a 
/// given <see cref="ComponentKind"/>.
/// </summary>
public class ComponentDeserialiserDispatcher : IComponentDeserialiserDispatcher
{
   #region Fields
   private readonly IMessageComponentDeserialiser _messageDeserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ComponentDeserialiserDispatcher"/>.</summary>
   /// <param name="messageDeserialiser">The message deserialiser to use.</param>
   public ComponentDeserialiserDispatcher(IMessageComponentDeserialiser messageDeserialiser)
   {
      _messageDeserialiser = messageDeserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IComponent Deserialise(BinaryReader reader, ComponentKind componentKind)
   {
      if (componentKind is ComponentKind.Message)
         return _messageDeserialiser.Deserialise(reader);

      throw new ArgumentException($"Unknown component kind ({componentKind}).");
   }
   #endregion
}
