using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;

namespace TNO.Logging.Common.Exceptions;

/// <summary>
/// Represents info about an <see cref="Exception"/>.
/// </summary>
public class ExceptionInfo : IExceptionInfo
{
   #region Properties
   /// <inheritdoc/>
   public ulong ExceptionTypeId { get; }

   /// <inheritdoc/>
   public ulong ExceptionDataTypeId { get; }

   /// <inheritdoc/>
   public Guid ExceptionGroupId { get; }

   /// <inheritdoc/>
   public string Message { get; }

   /// <inheritdoc/>
   public IStackTraceInfo StackTrace { get; }

   /// <inheritdoc/>
   public ITableInfo? AdditionalData { get; }

   /// <inheritdoc/>
   public IExceptionData Data { get; }

   /// <inheritdoc/>
   public IExceptionInfo? InnerException { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionInfo"/>.</summary>
   /// <param name="exceptionTypeId">The type id of the <see cref="Exception"/>.</param>
   /// <param name="exceptionDataTypeId">The type id of the <see cref="Exception"/> that was recognised by the <see cref="ExceptionGroupId"/>.</param>
   /// <param name="exceptionGroupId">The id of the exception group.</param>
   /// <param name="message">The <see cref="Exception.Message"/>.</param>
   /// <param name="stackTrace">The stack trace info about the <see cref="Exception"/>.</param>
   /// <param name="additionalData">The additional data contained in the <see cref="Exception.Data"/>.</param>
   /// <param name="data">The custom data related to the <see cref="Exception"/>.</param>
   /// <param name="innerException">The <see cref="Exception.InnerException"/>.</param>
   public ExceptionInfo(
      ulong exceptionTypeId,
      ulong exceptionDataTypeId,
      Guid exceptionGroupId,
      string message,
      IStackTraceInfo stackTrace,
      ITableInfo? additionalData,
      IExceptionData data,
      IExceptionInfo? innerException)
   {
      ExceptionTypeId = exceptionTypeId;
      ExceptionDataTypeId = exceptionDataTypeId;
      ExceptionGroupId = exceptionGroupId;
      Message = message;
      StackTrace = stackTrace;
      AdditionalData = additionalData;
      Data = data;
      InnerException = innerException;
   }
   #endregion
}