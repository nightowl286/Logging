using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.LogData.StackTraces;
using TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackTraceInfos;

namespace TNO.Logging.Reading.LogData.Methods.StackTraceInfos;

/// <summary>
/// A factory that should be used in instances of the see <see cref="IStackTraceInfoDeserialiser"/>.
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
