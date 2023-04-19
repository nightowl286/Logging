using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.StackTrace.Versions;

namespace TNO.Logging.Reading.Entries.Components.StackTrace;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IStackTraceComponent"/>.
/// </summary>
internal class StackTraceComponentDeserialiserSelector : DeserialiserSelectorBase<IStackTraceComponent>
{
   public StackTraceComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<StackTraceComponentDeserialiser0>(0);
   }
}
