using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.StackTraces.StackTraceInfos.Versions;

namespace TNO.Logging.Reading.LogData.StackTraces.StackTraceInfos;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IStackTraceInfo"/>.
/// </summary>
internal class StackTraceInfoDeserialiserSelector : DeserialiserSelectorBase<IStackTraceInfo>
{
   public StackTraceInfoDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<StackTraceInfoDeserialiser0>(0);
   }
}