using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Exceptions.ExceptionInfos;

/// <inheritdoc/>
public interface IExceptionInfoDeserialiser : IBinaryDeserialiser<IExceptionInfo>, IVersioned
{
}
