using System.Diagnostics;
using TNO.Logging.Common.Abstractions.LogData.Methods;

namespace TNO.Logging.Common.Abstractions.LogData.StackTraces;

/// <summary>
/// Denotes info about a <see cref="StackFrame"/>.
/// </summary>
public interface IStackFrameInfo
{
   #region Properties
   /// <summary>The id of the file where this frame is.</summary>
   /// <remarks>This might be <c>0</c> if the file wasn't available.</remarks>
   ulong FileId { get; }

   /// <summary>The line in the file where this frame is.</summary>
   /// <remarks>This might be <c>0</c> if the line number wasn't available.</remarks>
   uint LineInFile { get; }

   /// <summary>The column in the line where this frame is.</summary>
   /// <remarks>This might be <c>0</c> if the column wasn't available.</remarks>
   uint ColumnInLine { get; }

   /// <summary>The base info of the method where this frame is.</summary>
   IMethodBaseInfo? MainMethod { get; }

   /// <summary>The secondary base info of the method where this frame is.</summary>
   /// <remarks>This is used in situations like async/enumerator methods.</remarks>
   IMethodBaseInfo? SecondaryMethod { get; }
   #endregion
}
