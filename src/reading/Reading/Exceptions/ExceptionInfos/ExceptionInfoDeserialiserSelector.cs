using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Exceptions.ExceptionInfos;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Exceptions.ExceptionInfos.Versions;

namespace TNO.Logging.Reading.Exceptions.ExceptionInfos;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IExceptionInfo"/>.
/// </summary>
internal class ExceptionInfoDeserialiserSelector : DeserialiserSelectorBase<IExceptionInfoDeserialiser>, IExceptionInfoDeserialiserSelector
{
   public ExceptionInfoDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<ExceptionInfoDeserialiser0>(0);
   }
}
