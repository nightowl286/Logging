using System;
using System.Threading;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <summary>
/// Denotes a converter between different <see cref="Exception"/> types into their corresponding <see cref="IExceptionInfo"/>.
/// </summary>
public interface IExceptionInfoConverter
{
   #region Methods
   /// <summary>Gets the <see cref="IExceptionInfo"/> for the given <paramref name="exception"/>.</summary>
   /// <param name="exception">The exception to get the <see cref="IExceptionInfo"/> for.</param>
   /// <param name="threadId">
   /// The <see cref="Thread.ManagedThreadId"/> of the thread
   /// that the <paramref name="exception"/> was thrown on.
   /// </param>
   /// <returns>The <see cref="IExceptionInfo"/> for the given <paramref name="exception"/>.</returns>
   IExceptionInfo Convert(Exception exception, int? threadId);
   #endregion
}
