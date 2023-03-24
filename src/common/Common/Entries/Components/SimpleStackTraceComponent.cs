using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.SimpleStackTrace"/> component.
/// </summary>
/// <param name="StackTrace">The stack trace information.</param>
/// <param name="ThreadId">
/// The <see cref="Thread.ManagedThreadId"/> of the 
/// thread that the <see cref="StackTrace"/> is from.
/// </param>
public record class SimpleStackTraceComponent(string StackTrace, int ThreadId) : ISimpleStackTraceComponent
{
   #region Properties
   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.SimpleStackTrace;
   #endregion
}
