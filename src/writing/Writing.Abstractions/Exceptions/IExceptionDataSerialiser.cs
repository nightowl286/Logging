using System;
using System.IO;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <summary>
/// Denotes a general <see cref="IExceptionData"/> serialiser.
/// </summary>
public interface IExceptionDataSerialiser
{
   #region Methods
   /// <summary>Serialises the given <paramref name="data"/> using the given <paramref name="writer"/>.</summary>
   /// <param name="writer">The writer to use.</param>
   /// <param name="exceptionGroupId">The id of the exception group that was used to convert the <paramref name="data"/>.</param>
   /// <param name="data">The data to serialise.</param>
   void Serialise(BinaryWriter writer, IExceptionData data, Guid exceptionGroupId);

   /// <summary>Calculates the amount of bytes the given <paramref name="data"/> requires.</summary>
   /// <param name="data">The data to calculate the serialised size for.</param>
   /// <param name="exceptionGroupId">The id of the exception group that was used to convert the <paramref name="data"/>.</param>
   /// <returns>The amount of bytes the given <paramref name="data"/> requires.</returns>
   ulong Count(IExceptionData data, Guid exceptionGroupId);
   #endregion
}
