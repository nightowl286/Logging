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
   #region Properties
   /// <inheritdoc/>
   protected override Dictionary<uint, Type> DeserialiserTypes { get; } = new Dictionary<uint, Type>
   {
      { 0, typeof(MessageComponentDeserialiser0) }
   };
   #endregion
}
