using System;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <summary>
/// Converts different <see cref="Exception"/> types into their corresponding <see cref="IExceptionInfo"/>.
/// </summary>
public interface IExceptionInfoConverter
{
   #region Methods
   /// <summary>Gets the <see cref="IExceptionInfo"/> for the given <paramref name="exception"/>.</summary>
   /// <param name="exception">The exception to get the <see cref="IExceptionInfo"/> for.</param>
   /// <returns>The <see cref="IExceptionInfo"/> for the given <paramref name="exception"/>.</returns>
   IExceptionInfo Convert(Exception exception);
   #endregion
}
