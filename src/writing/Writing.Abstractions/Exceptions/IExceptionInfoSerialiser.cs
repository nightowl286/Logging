using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <inheritdoc/>
public interface IExceptionInfoSerialiser : IBinarySerialiser<IExceptionInfo>
{
}
