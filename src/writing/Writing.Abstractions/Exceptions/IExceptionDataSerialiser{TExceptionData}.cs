using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <inheritdoc/>
public interface IExceptionDataSerialiser<TExceptionData> : ISerialiser<TExceptionData>
   where TExceptionData : IExceptionData
{
}
