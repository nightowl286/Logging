using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Exceptions.ExceptionInfos;

/// <inheritdoc/>
public interface IExceptionInfoDeserialiserSelector : IDeserialiserSelector<IExceptionInfoDeserialiser>
{
}
