using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Writing.Abstractions.Exceptions;

namespace TNO.Logging.Writing.Exceptions;

/// <summary>
/// Represents information about an implementation of the <see cref="IExceptionDataHandler{TException, TExceptionData}"/>.
/// </summary>
public class ExceptionDataHandlerInfo : IExceptionDataHandlerInfo
{
   #region Properties
   /// <inheritdoc/>
   public Guid Id { get; }

   /// <inheritdoc/>
   public Type ExceptionType { get; }

   /// <inheritdoc/>
   public Type ExceptionDataType { get; }

   /// <inheritdoc/>
   public Type HandlerType { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionDataHandlerInfo"/>.</summary>
   /// <param name="id">The id of the handler.</param>
   /// <param name="exceptionType">The type of the exceptions that the handler can convert.</param>
   /// <param name="exceptionDataType">The type of the <see cref="IExceptionData"/> that the handler uses.</param>
   /// <param name="handlerType">The type of the handler itself.</param>
   public ExceptionDataHandlerInfo(Guid id, Type exceptionType, Type exceptionDataType, Type handlerType)
   {
      Id = id;
      ExceptionType = exceptionType;
      ExceptionDataType = exceptionDataType;
      HandlerType = handlerType;
   }
   #endregion
}