using System.Diagnostics;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;

namespace TNO.Logging.Common.LogData.StackTraces;

/// <summary>
/// Represents info about a <see cref="StackFrame"/>.
/// </summary>
/// <param name="FileId">The id of the file where this frame is.</param>
/// <param name="LineInFile">The line in the file where this frame is.</param>
/// <param name="ColumnInLine">The column in the line where this frame is.</param>
/// <param name="MainMethod">The base info of the method where this frame is.</param>
/// <param name="SecondaryMethod">The secondary base info of the method where this frame is.</param>
public record class StackFrameInfo(
   ulong FileId,
   uint LineInFile,
   uint ColumnInLine,
   IMethodBaseInfo MainMethod,
   IMethodBaseInfo? SecondaryMethod) : IStackFrameInfo;
