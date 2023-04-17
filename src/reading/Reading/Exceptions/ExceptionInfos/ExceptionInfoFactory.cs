using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Exceptions;
using TNO.Logging.Reading.Abstractions.Exceptions.ExceptionInfos;

namespace TNO.Logging.Reading.Exceptions.ExceptionInfos;

/// <summary>
/// A factory that should be used in instances of the <see cref="IExceptionInfoDeserialiser"/>.
/// </summary>
internal static class ExceptionInfoFactory
{
   #region Functions
   public static IExceptionInfo Version0(
      ulong exceptionTypeId,
      ulong exceptionDataTypeId,
      Guid exceptionGroup,
      string message,
      IStackTraceInfo stackTrace,
      ITableInfo additionalData,
      IExceptionData data,
      IExceptionInfo? innerException)
   {
      return new ExceptionInfo(
         exceptionTypeId,
         exceptionDataTypeId,
         exceptionGroup,
         message,
         stackTrace,
         additionalData,
         data,
         innerException);
   }
   #endregion
}
