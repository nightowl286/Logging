using System.Diagnostics;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Exceptions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Exceptions;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.Loggers;

namespace TNO.Logging.Writing.Exceptions;

/// <summary>
/// Represents a converter between different <see cref="Exception"/> types into their corresponding <see cref="IExceptionInfo"/>.
/// </summary>
public class ExceptionInfoConverter : IExceptionInfoConverter
{
   #region Fields
   private readonly IExceptionDataConverter _exceptionDataConverter;
   private readonly ILogWriteContext _writeContext;
   private readonly ILogDataCollector _dataCollector;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionInfoConverter"/>.</summary>
   /// <param name="writeContext">The <see cref="ILogWriteContext"/> to use.</param>
   /// <param name="dataCollector">The <see cref="ILogDataCollector"/> to use.</param>
   /// <param name="exceptionDataConverter"><see cref="IExceptionDataConverter"/> to use.</param>
   public ExceptionInfoConverter(
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector,
      IExceptionDataConverter exceptionDataConverter)
   {
      _exceptionDataConverter = exceptionDataConverter;
      _writeContext = writeContext;
      _dataCollector = dataCollector;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IExceptionInfo Convert(Exception exception, int? threadId)
   {
      ulong exceptionTypeId = TypeInfoHelper.EnsureIdsForAssociatedTypes(_writeContext, _dataCollector, exception.GetType());
      string message = exception.Message;

      StackTrace stackTrace = new StackTrace(exception, true);
      IStackTraceInfo stackTraceInfo = StackTraceInfoHelper.GetStackTraceInfo(_writeContext, _dataCollector, stackTrace, threadId ?? -1);

      ITableInfo additionalData = TableInfoHelper.Convert(_writeContext, _dataCollector, exception.Data);
      IExceptionData exceptionData = _exceptionDataConverter.Convert(exception, out ulong exceptionDataTypeId, out Guid exceptionGroup);

      IExceptionInfo? innerExceptionInfo =
         exception.InnerException is null ?
         null :
         Convert(exception.InnerException, null);

      return new ExceptionInfo(
         exceptionTypeId,
         exceptionDataTypeId,
         exceptionGroup,
         message,
         stackTraceInfo,
         additionalData,
         exceptionData,
         innerExceptionInfo);
   }
   #endregion
}
