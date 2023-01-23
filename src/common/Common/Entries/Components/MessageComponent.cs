using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Message"/> component.
/// </summary>
/// <param name="Message">The message of this component.</param>
public record class MessageComponent(string Message) : IMessageComponent
{
   #region Properties
   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Message;
   #endregion
}
