using System;
using System.IO;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Reading.Abstractions.Exceptions;

public interface IExceptionDataDeserialiser
{
   #region Methods
   IExceptionData Deserialise(BinaryReader reader, Guid exceptionGroup);
   #endregion
}
