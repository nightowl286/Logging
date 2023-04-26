using System.Diagnostics;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;

namespace TNO.Logging.Common.LogData.StackTraces;

/// <summary>
/// Represents info about a <see cref="StackTrace"/>.
/// </summary>
public record class StackTraceInfo : IStackTraceInfo
{
   #region Properties
   /// <inheritdoc/>
   public int ThreadId { get; }

   /// <inheritdoc/>
   public IReadOnlyList<IStackFrameInfo> Frames { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackTraceInfo"/>.</summary>
   /// <param name="threadId">The <see cref="Thread.ManagedThreadId"/> of the thread that this stack trace is from.</param>
   /// <param name="frames">The frames that make up this stack trace.</param>
   public StackTraceInfo(int threadId, IReadOnlyList<IStackFrameInfo> frames)
   {
      ThreadId = threadId;
      Frames = frames;
   }
   #endregion
}
