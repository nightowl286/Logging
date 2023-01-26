using System.Diagnostics;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Entries.Components;
internal class ComponentSerialiserDispatcher : IComponentSerialiserDispatcher
{
   #region Fields
   private readonly IMessageComponentSerialiser _messageSerialiser;
   #endregion
   public ComponentSerialiserDispatcher(IMessageComponentSerialiser messageSerialiser)
   {
      _messageSerialiser = messageSerialiser;
   }

   #region Methods
   public void Serialise(BinaryWriter writer, IComponent data)
   {
      if (data is IMessageComponent messageComponent)
      {
         Debug.Assert(messageComponent.Kind is ComponentKind.Message);
         _messageSerialiser.Serialise(writer, messageComponent);
      }
      else
         throw new ArgumentException($"Unknown component type ({data.GetType()}). Kind: {data.Kind}.", nameof(data));
   }
   #endregion
}

