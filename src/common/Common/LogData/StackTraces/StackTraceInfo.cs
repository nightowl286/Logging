using System.Diagnostics;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;

namespace TNO.Logging.Common.LogData.StackTraces;

/// <summary>
/// Represents info about a <see cref="StackTrace"/>.
/// </summary>
/// <param name="ThreadId">The <see cref="Thread.ManagedThreadId"/> of the thread that this stack trace is from.</param>
/// <param name="Frames">The frames that make up this stack trace.</param>
public record class StackTraceInfo(
   int ThreadId,
   IReadOnlyList<IStackFrameInfo> Frames) : IStackTraceInfo;
