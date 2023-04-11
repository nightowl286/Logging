using TNO.Logging.Common.Abstractions.LogData.StackTraces;

namespace TNO.Logging.Common.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ComponentKind.StackTrace"/> component.
/// </summary>
public interface IStackTraceComponent : IComponent
{
   #region Properties
   /// <summary>The stack trace information.</summary>
   IStackTraceInfo StackTrace { get; }
   #endregion
}
