using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Exceptions;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.StackTraces;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Tables;
using TNO.Logging.Writing.Serialisers;

namespace TNO.Logging.Writing.Exceptions;

/// <summary>
/// Represents a serialiser for <see cref="IExceptionInfo"/>.
/// </summary>
[Version(0)]
public class ExceptionInfoSerialiser : IExceptionInfoSerialiser
{
   #region Fields
   private readonly IExceptionDataSerialiser _exceptionDataSerialiser;
   private readonly IStackTraceInfoSerialiser _stackTraceInfoSerialiser;
   private readonly ITableInfoSerialiser _tableInfoSerialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionInfoSerialiser"/>.</summary>
   /// <param name="exceptionDataSerialiser">The <see cref="IExceptionDataSerialiser"/> to use.</param>
   /// <param name="stackTraceInfoSerialiser">The <see cref="IStackTraceInfoSerialiser"/> to use.</param>
   /// <param name="tableInfoSerialiser">The <see cref="ITableInfoSerialiser"/> to use.</param>
   public ExceptionInfoSerialiser(
      IExceptionDataSerialiser exceptionDataSerialiser,
      IStackTraceInfoSerialiser stackTraceInfoSerialiser,
      ITableInfoSerialiser tableInfoSerialiser)
   {
      _exceptionDataSerialiser = exceptionDataSerialiser;
      _stackTraceInfoSerialiser = stackTraceInfoSerialiser;
      _tableInfoSerialiser = tableInfoSerialiser;
   }
   #endregion

   #region Methods
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

      _stackTraceInfoSerialiser.Serialise(writer, stackTraceInfo);
      _tableInfoSerialiser.Serialise(writer, additionalData);

      _exceptionDataSerialiser.Serialise(writer, exceptionData, data.ExceptionGroupId);

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
      ulong stackTraceSize = _stackTraceInfoSerialiser.Count(data.StackTrace);
      ulong tableSize = _tableInfoSerialiser.Count(data.AdditionalData);
      ulong exceptionDataSize = _exceptionDataSerialiser.Count(data.Data, data.ExceptionGroupId);
      ulong innerExceptionSize = data.InnerException is null ? 0 : Count(data.InnerException);

      return size + (ulong)messageSize + stackTraceSize + tableSize + exceptionDataSize + innerExceptionSize;
   }
   #endregion
}
