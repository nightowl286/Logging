using System;
using System.IO;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Reading.Abstractions.Exceptions;

/// <summary>
/// Denotes a general deserialiser for <see cref="IExceptionData"/>.
/// </summary>
public interface IExceptionDataDeserialiser
{
   #region Methods
   /// <summary>Deserialises an instance of <see cref="IExceptionData"/> using the given <paramref name="reader"/>.</summary>
   /// <param name="reader">The reader to use.</param>
   /// <param name="id">The id of the <see cref="IExceptionDataDeserialiser{TExceptionData}"/> to use.</param>
   /// <returns>The deserialised <see cref="IExceptionData"/>.</returns>
   IExceptionData Deserialise(BinaryReader reader, Guid id);
   #endregion
}
