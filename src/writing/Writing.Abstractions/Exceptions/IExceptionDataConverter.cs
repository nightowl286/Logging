using System;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <summary>
/// Denotes an general <see cref="IExceptionData"/> converter.
/// </summary>
public interface IExceptionDataConverter
{
   #region Methods
   /// <summary>Converts the given <paramref name="exception"/> to a corresponding <see cref="IExceptionData"/>.</summary>
   /// <param name="exception">The exception to convert.</param>
   /// <param name="exceptionDataTypeId">The type id of the exception that was recognised by the <paramref name="exceptionGroupId"/>.</param>
   /// <param name="exceptionGroupId">The id of the exception group that converted the <paramref name="exception"/>.</param>
   /// <returns>The converted <see cref="IExceptionData"/>.</returns>
   IExceptionData Convert(Exception exception, out ulong exceptionDataTypeId, out Guid exceptionGroupId);
   #endregion
}
