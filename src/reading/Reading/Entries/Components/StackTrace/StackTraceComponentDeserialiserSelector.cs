using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.Components.StackTrace;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.StackTrace.Versions;

namespace TNO.Logging.Reading.Entries.Components.StackTrace;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IStackTraceComponentDeserialiser"/>.
/// </summary>
internal class StackTraceComponentDeserialiserSelector : DeserialiserSelectorBase<IStackTraceComponentDeserialiser>, IStackTraceComponentDeserialiserSelector
{
   public StackTraceComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<StackTraceComponentDeserialiser0>(0);
   }
}
