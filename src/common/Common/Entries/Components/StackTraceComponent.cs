using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.StackTrace"/> component.
/// </summary>
/// <param name="StackTrace">The stack trace information.</param>
public record class StackTraceComponent(IStackTraceInfo StackTrace) : IStackTraceComponent
{
   #region Properties
   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.StackTrace;
   #endregion
}
