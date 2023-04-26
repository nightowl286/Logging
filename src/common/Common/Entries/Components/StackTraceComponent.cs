using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.StackTrace"/> component.
/// </summary>
public class StackTraceComponent : IStackTraceComponent
{
   #region Properties
   /// <inheritdoc/>
   public IStackTraceInfo StackTrace { get; }

   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.StackTrace;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackTraceComponent"/>.</summary>
   /// <param name="stackTrace">The stack trace information.</param>
   public StackTraceComponent(IStackTraceInfo stackTrace)
   {
      StackTrace = stackTrace;
   }
   #endregion
}
