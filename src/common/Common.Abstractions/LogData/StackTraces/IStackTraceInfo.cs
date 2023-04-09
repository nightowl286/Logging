using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TNO.Logging.Common.Abstractions.LogData.StackTraces;

/// <summary>
/// Denotes info about a <see cref="StackTrace"/>.
/// </summary>
public interface IStackTraceInfo
{
   #region Properties
   /// <summary>The <see cref="Thread.ManagedThreadId"/> of the thread that this stack trace is from.</summary>
   /// <remarks>This value will be negative in situations where the thread is not known.</remarks>
   int ThreadId { get; }

   /// <summary>The frames that make up this stack trace.</summary>
   IReadOnlyList<IStackFrameInfo> Frames { get; }
   #endregion
}
