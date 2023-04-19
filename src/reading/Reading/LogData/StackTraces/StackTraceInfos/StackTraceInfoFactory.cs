using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.LogData.StackTraces;

namespace TNO.Logging.Reading.LogData.Methods.StackTraceInfos;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="IStackTraceInfo"/>.
/// </summary>
internal static class StackTraceInfoFactory
{
   #region Functions
   public static IStackTraceInfo Version0(int threadId, IReadOnlyList<IStackFrameInfo> frames)
   {
      return new StackTraceInfo(
         threadId,
         frames);
   }
   #endregion
}
