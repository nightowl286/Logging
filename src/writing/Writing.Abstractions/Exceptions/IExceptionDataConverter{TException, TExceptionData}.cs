using System;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <summary>
/// Denotes an exception data converter, that converts exceptions
/// of the type <typeparamref name="TException"/> to <typeparamref name="TExceptionData"/>.
/// </summary>
/// <typeparam name="TException">The type of the <see cref="Exception"/>.</typeparam>
/// <typeparam name="TExceptionData">The type of the <see cref="IExceptionData"/>.</typeparam>
public interface IExceptionDataConverter<in TException, out TExceptionData>
   where TException : Exception
   where TExceptionData : IExceptionData
{
   #region Methods
   /// <summary>Converts the given <paramref name="exception"/> to the corresponding <typeparamref name="TExceptionData"/>.</summary>
   /// <param name="exception">The <typeparamref name="TException"/> to convert.</param>
   /// <returns>The converted <typeparamref name="TExceptionData"/>.</returns>
   TExceptionData Convert(TException exception);
   #endregion
}
