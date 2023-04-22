using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Writing.Abstractions.Exceptions;

namespace TNO.Logging.Writing.Exceptions;

/// <summary>
/// Represents information about an implementation of the <see cref="IExceptionDataHandler{TException, TExceptionData}"/>.
/// </summary>
/// <param name="Id">The id of the handler.</param>
/// <param name="ExceptionType">The type of the exceptions that the handler can convert.</param>
/// <param name="ExceptionDataType">The type of the <see cref="IExceptionData"/> that the handler uses.</param>
/// <param name="HandlerType">The type of the handler itself.</param>
public record ExceptionDataHandlerInfo(
   Guid Id,
   Type ExceptionType,
   Type ExceptionDataType,
   Type HandlerType) : IExceptionDataHandlerInfo;