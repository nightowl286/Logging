using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.LogData.Tables;

namespace TNO.Logging.Common.Exceptions;

/// <summary>
/// Represents info about an <see cref="Exception"/>.
/// </summary>
/// <param name="ExceptionTypeId">The type id of the <see cref="Exception"/>.</param>
/// <param name="ExceptionDataTypeId">The type id of the <see cref="Exception"/> that was recognised by the <see cref="ExceptionGroupId"/>.</param>
/// <param name="ExceptionGroupId">The id of the exception group.</param>
/// <param name="Message">The <see cref="Exception.Message"/>.</param>
/// <param name="StackTrace">The stack trace info about the <see cref="Exception"/>.</param>
/// <param name="AdditionalData">The additional data contained in the <see cref="Exception.Data"/>.</param>
/// <param name="Data">The custom data related to the <see cref="Exception"/>.</param>
/// <param name="InnerException">The <see cref="Exception.InnerException"/>.</param>
public record class ExceptionInfo(
   ulong ExceptionTypeId,
   ulong ExceptionDataTypeId,
   Guid ExceptionGroupId,
   string Message,
   IStackTraceInfo StackTrace,
   ITableInfo AdditionalData,
   IExceptionData Data,
   IExceptionInfo? InnerException) : IExceptionInfo;