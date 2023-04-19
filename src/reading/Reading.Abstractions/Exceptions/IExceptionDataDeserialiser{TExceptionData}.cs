using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Exceptions;

/// <inheritdoc/>
public interface IExceptionDataDeserialiser<TExceptionData> : IDeserialiser<TExceptionData>
   where TExceptionData : IExceptionData
{
}