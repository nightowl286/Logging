using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.Components.SimpleStackTrace;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.SimpleStackTrace.Versions;

namespace TNO.Logging.Reading.Entries.Components.SimpleStackTrace;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="ISimpleStackTraceComponentDeserialiser"/>.
/// </summary>
internal class SimpleStackTraceComponentDeserialiserSelector : DeserialiserSelectorBase<ISimpleStackTraceComponentDeserialiser>, ISimpleStackTraceComponentDeserialiserSelector
{
   public SimpleStackTraceComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<SimpleStackTraceComponentDeserialiser0>(0);
   }
}
