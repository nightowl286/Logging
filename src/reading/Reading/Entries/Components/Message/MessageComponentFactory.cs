using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Message;

namespace TNO.Logging.Reading.Entries.Components.Message;

/// <summary>
/// A factory class that should be used instances of the <see cref="IMessageComponentDeserialiser"/>.
/// </summary>
internal static class MessageComponentFactory
{
   #region Functions
   public static IMessageComponent Version0(string message) => new MessageComponent(message);
   #endregion
}
