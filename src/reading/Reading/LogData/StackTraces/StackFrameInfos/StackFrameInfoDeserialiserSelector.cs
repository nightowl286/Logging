using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackFrameInfos;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.StackTraces.StackFrameInfos.Versions;

namespace TNO.Logging.Reading.LogData.StackTraces.StackFrameInfos;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IStackFrameInfo"/>.
/// </summary>
internal class StackFrameInfoDeserialiserSelector : DeserialiserSelectorBase<IStackFrameInfoDeserialiser>, IStackFrameInfoDeserialiserSelector
{
   public StackFrameInfoDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<StackFrameInfoDeserialiser0>(0);
   }
}