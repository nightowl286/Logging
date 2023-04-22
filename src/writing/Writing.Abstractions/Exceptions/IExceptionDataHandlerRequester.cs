using System;
using System.Diagnostics.CodeAnalysis;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <summary>
/// Denotes a requester for types that implement <see cref="IExceptionDataHandler{TException, TExceptionData}"/>.
/// </summary>
public interface IExceptionDataHandlerRequester
{
   #region Methods
   /// <summary>
   /// Tries to get the <paramref name="info"/> about an <see cref="IExceptionDataHandler{TException, TExceptionData}"/> 
   /// with the given <paramref name="exceptionType"/>.
   /// </summary>
   /// <param name="exceptionType">The type of the exception that the handler can convert.</param>
   /// <param name="info">
   /// The info about an <see cref="IExceptionDataHandler{TException, TExceptionData}"/>,
   /// or <see langword="null"/> if this method returns <see langword="false"/>.
   /// </param>
   /// <returns>
   /// <see langword="true"/> if the <paramref name="info"/> could be found, <see langword="false"/> otherwise.
   /// </returns>
   bool ByExceptionType(Type exceptionType, [NotNullWhen(true)] out IExceptionDataHandlerInfo? info);

   /// <summary>
   /// Tries to get the <paramref name="info"/> about an <see cref="IExceptionDataHandler{TException, TExceptionData}"/> 
   /// with the given <paramref name="id"/>.
   /// </summary>
   /// <param name="id">The id of the handler.</param>
   /// <param name="info">
   /// The info about an <see cref="IExceptionDataHandler{TException, TExceptionData}"/>,
   /// or <see langword="null"/> if this method returns <see langword="false"/>.
   /// </param>
   /// <returns>
   /// <see langword="true"/> if the <paramref name="info"/> could be found, <see langword="false"/> otherwise.
   /// </returns>
   bool ById(Guid id, [NotNullWhen(true)] out IExceptionDataHandlerInfo? info);
   #endregion
}
