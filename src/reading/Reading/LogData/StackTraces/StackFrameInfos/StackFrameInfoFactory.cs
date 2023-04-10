using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.LogData.StackTraces;
using TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackFrameInfos;

namespace TNO.Logging.Reading.LogData.Methods.StackFrameInfos;

/// <summary>
/// A factory that should be used in instances of the see <see cref="IStackFrameInfoDeserialiser"/>.
/// </summary>
internal static class StackFrameInfoFactory
{
   #region Functions
   public static IStackFrameInfo Version0(
      ulong fileId,
      uint line,
      uint column,
      IMethodBaseInfo? mainMethod,
      IMethodBaseInfo? secondaryMethod)
   {
      return new StackFrameInfo(
         fileId,
         line,
         column,
         mainMethod,
         secondaryMethod);
   }
   #endregion
}
