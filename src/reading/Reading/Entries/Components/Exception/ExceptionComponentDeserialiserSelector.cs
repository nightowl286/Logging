using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.Exception.Versions;

namespace TNO.Logging.Reading.Entries.Components.Exception;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IExceptionComponent"/>.
/// </summary>
internal class ExceptionComponentDeserialiserSelector : DeserialiserSelectorBase<IExceptionComponent>
{
   public ExceptionComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<ExceptionComponentDeserialiser0>(0);
   }
}