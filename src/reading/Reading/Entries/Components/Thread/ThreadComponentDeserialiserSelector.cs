using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.Components.Thread;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.Thread.Versions;

namespace TNO.Logging.Reading.Entries.Components.Thread;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IThreadComponentDeserialiser"/>.
/// </summary>
internal class ThreadComponentDeserialiserSelector : DeserialiserSelectorBase<IThreadComponentDeserialiser>, IThreadComponentDeserialiserSelector
{
   public ThreadComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<ThreadComponentDeserialiser0>(0);
   }
}