using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;

namespace TNO.Logging.Reading.Entries.Components.Message;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="IMessageComponent"/>.
/// </summary>
internal static class MessageComponentFactory
{
   #region Functions
   public static IMessageComponent Version0(string message) => new MessageComponent(message);
   #endregion
}