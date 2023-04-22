using System;
using System.IO;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <summary>
/// Denotes an general <see cref="IExceptionData"/> converter.
/// </summary>
public interface IExceptionDataHandler
{
   #region Methods
   /// <summary>Converts the given <paramref name="exception"/> to a corresponding <see cref="IExceptionData"/>.</summary>
   /// <param name="exception">The exception to convert.</param>
   /// <param name="exceptionDataTypeId">The type id of the exception that was recognised by the <paramref name="handlerId"/>.</param>
   /// <param name="handlerId">The id of the exception data handler that converted the <paramref name="exception"/>.</param>
   /// <returns>The converted <see cref="IExceptionData"/>.</returns>
   IExceptionData Convert(Exception exception, out ulong exceptionDataTypeId, out Guid handlerId);

   /// <summary>Serialises the given <paramref name="data"/> using the given <paramref name="writer"/>.</summary>
   /// <param name="writer">The writer to use.</param>
   /// <param name="handlerId">The id of the exception data handler that was used to convert the <paramref name="data"/>.</param>
   /// <param name="data">The data to serialise.</param>
   void Serialise(BinaryWriter writer, IExceptionData data, Guid handlerId);

   /// <summary>Calculates the amount of bytes the given <paramref name="data"/> requires.</summary>
   /// <param name="data">The data to calculate the serialised size for.</param>
   /// <param name="handlerId">The id of the exception data handler that was used to convert the <paramref name="data"/>.</param>
   /// <returns>The amount of bytes the given <paramref name="data"/> requires.</returns>
   ulong Count(IExceptionData data, Guid handlerId);
   #endregion
}
