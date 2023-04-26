using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Message"/> component.
/// </summary>
public class MessageComponent : IMessageComponent
{
   #region Properties
   /// <inheritdoc/>
   public string Message { get; }

   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Message;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="MessageComponent"/>.</summary>
   /// <param name="message">The message of this component.</param>
   public MessageComponent(string message)
   {
      Message = message;
   }
   #endregion
}