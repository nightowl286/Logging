using System.Diagnostics;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Common.Exceptions;
using TNO.Logging.Logging.Helpers;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Exceptions;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Serialisers;

namespace TNO.Logging.Writing.Exceptions;

/// <summary>
/// Represents a converter between different <see cref="Exception"/> types into their corresponding <see cref="IExceptionInfo"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.ExceptionInfo)]
public class ExceptionInfoHandler : IExceptionInfoHandler
{
   #region Fields
   private readonly IExceptionDataHandler _exceptionDataHandler;
   private readonly ISerialiser _serialiser;
   private readonly ILogWriteContext _writeContext;
   private readonly ILogDataCollector _dataCollector;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionInfoHandler"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   /// <param name="writeContext">The <see cref="ILogWriteContext"/> to use.</param>
   /// <param name="dataCollector">The <see cref="ILogDataCollector"/> to use.</param>
   /// <param name="exceptionDataHandler"><see cref="IExceptionDataHandler"/> to use.</param>
   public ExceptionInfoHandler(
      ISerialiser serialiser,
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector,
      IExceptionDataHandler exceptionDataHandler)
   {
      _serialiser = serialiser;
      _exceptionDataHandler = exceptionDataHandler;
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
      IExceptionData exceptionData = _exceptionDataHandler.Convert(exception, out ulong exceptionDataTypeId, out Guid exceptionGroupId);

      IExceptionInfo? innerExceptionInfo =
         exception.InnerException is null ?
         null :
         Convert(exception.InnerException, null);

      return new ExceptionInfo(
         exceptionTypeId,
         exceptionDataTypeId,
         exceptionGroupId,
         message,
         stackTraceInfo,
         additionalData,
         exceptionData,
         innerExceptionInfo);
   }

   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IExceptionInfo data)
   {
      ulong exceptionTypeId = data.ExceptionTypeId;
      ulong exceptionDataTypeId = data.ExceptionDataTypeId;
      Guid exceptionGroupId = data.ExceptionGroupId;

      string message = data.Message;
      IStackTraceInfo stackTraceInfo = data.StackTrace;
      ITableInfo additionalData = data.AdditionalData;
      IExceptionData exceptionData = data.Data;
      IExceptionInfo? innerException = data.InnerException;

      writer.Write(exceptionTypeId);
      writer.Write(exceptionDataTypeId);
      writer.Write(exceptionGroupId);

      writer.Write(message);

      _serialiser.Serialise(writer, stackTraceInfo);
      _serialiser.Serialise(writer, additionalData);

      _exceptionDataHandler.Serialise(writer, exceptionData, data.ExceptionGroupId);

      if (writer.TryWriteNullable(innerException))
         Serialise(writer, innerException);
   }

   /// <inheritdoc/>
   public ulong Count(IExceptionInfo data)
   {
      ulong size =
         (sizeof(ulong) * 2) +
         sizeof(bool) +
         BinaryWriterSizeHelper.GuidSize;

      int messageSize = BinaryWriterSizeHelper.StringSize(data.Message);
      ulong stackTraceSize = _serialiser.Count(data.StackTrace);
      ulong tableSize = _serialiser.Count(data.AdditionalData);
      ulong exceptionDataSize = _exceptionDataHandler.Count(data.Data, data.ExceptionGroupId);
      ulong innerExceptionSize = data.InnerException is null ? 0 : Count(data.InnerException);

      return size + (ulong)messageSize + stackTraceSize + tableSize + exceptionDataSize + innerExceptionSize;
   }
   #endregion
}
