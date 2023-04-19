using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.LogData.StackTraces;

namespace TNO.Logging.Reading.LogData.Methods.StackFrameInfos;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="IStackFrameInfo"/>.
/// </summary>
internal static class StackFrameInfoFactory
{
   #region Functions
   public static IStackFrameInfo Version0(
      ulong fileId,
      uint line,
      uint column,
      IMethodBaseInfo mainMethod,
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
