using TNO.Common.Abstractions.Components;
using TNO.Common.Abstractions.Components.Kinds;

namespace TNO.Logging.Writer.Entries.Components.Kinds;

internal record MessageComponent(string Message) : IEntryComponent, IMessageComponent
{
   #region Properties
   public ComponentKind Kind => ComponentKind.Message;
   #endregion
}
