using System.Diagnostics;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// Denotes a <see cref="ISerialiser{T}"/> dispatcher that
/// will serialise a given <see cref="IComponent"/> based
/// on its <see cref="IComponent.Kind"/>.
/// </summary>
public class ComponentSerialiserDispatcher : IComponentSerialiserDispatcher
{
   #region Fields
   private readonly IMessageComponentSerialiser _messageSerialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ComponentSerialiserDispatcher"/>.</summary>
   /// <param name="messageSerialiser">The message serialiser to use.</param>
   public ComponentSerialiserDispatcher(IMessageComponentSerialiser messageSerialiser)
   {
      _messageSerialiser = messageSerialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
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

