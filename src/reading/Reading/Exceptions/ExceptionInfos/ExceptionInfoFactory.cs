using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Exceptions;

namespace TNO.Logging.Reading.Exceptions.ExceptionInfos;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="IExceptionInfo"/>.
/// </summary>
internal static class ExceptionInfoFactory
{
   #region Functions
   public static IExceptionInfo Version0(
      ulong exceptionTypeId,
      ulong exceptionDataTypeId,
      Guid exceptionGroupId,
      string message,
      IStackTraceInfo stackTrace,
      ITableInfo? additionalData,
      IExceptionData data,
      IExceptionInfo? innerException)
   {
      return new ExceptionInfo(
         exceptionTypeId,
         exceptionDataTypeId,
         exceptionGroupId,
         message,
         stackTrace,
         additionalData,
         data,
         innerException);
   }
   #endregion
}
