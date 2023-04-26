using System.Diagnostics;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;

namespace TNO.Logging.Common.LogData.StackTraces;

/// <summary>
/// Represents info about a <see cref="StackFrame"/>.
/// </summary>
public class StackFrameInfo : IStackFrameInfo
{
   #region Properties
   /// <inheritdoc/>
   public ulong FileId { get; }

   /// <inheritdoc/>
   public uint LineInFile { get; }

   /// <inheritdoc/>
   public uint ColumnInLine { get; }

   /// <inheritdoc/>
   public IMethodBaseInfo MainMethod { get; }

   /// <inheritdoc/>
   public IMethodBaseInfo? SecondaryMethod { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackFrameInfo"/>.</summary>
   /// <param name="fileId">The id of the file where this frame is.</param>
   /// <param name="lineInFile">The line in the file where this frame is.</param>
   /// <param name="columnInLine">The column in the line where this frame is.</param>
   /// <param name="mainMethod">The base info of the method where this frame is.</param>
   /// <param name="secondaryMethod">The secondary base info of the method where this frame is.</param>
   public StackFrameInfo(
      ulong fileId,
      uint lineInFile,
      uint columnInLine,
      IMethodBaseInfo mainMethod,
      IMethodBaseInfo? secondaryMethod)
   {
      FileId = fileId;
      LineInFile = lineInFile;
      ColumnInLine = columnInLine;
      MainMethod = mainMethod;
      SecondaryMethod = secondaryMethod;
   }
   #endregion
}
