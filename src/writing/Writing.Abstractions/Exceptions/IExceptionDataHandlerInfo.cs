using System;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <summary>
/// Denotes information about an implementation of the <see cref="IExceptionDataHandler{TException, TExceptionData}"/>.
/// </summary>
public interface IExceptionDataHandlerInfo
{
   #region Properties
   /// <summary>The id of the handler.</summary>
   Guid Id { get; }

   /// <summary>The type of the exceptions that the handler can convert.</summary>
   Type ExceptionType { get; }

   /// <summary>The type of the <see cref="IExceptionData"/> that the handler uses.</summary>
   Type ExceptionDataType { get; }

   /// <summary>The type of the handler itself.</summary>
   Type HandlerType { get; }
   #endregion
}
