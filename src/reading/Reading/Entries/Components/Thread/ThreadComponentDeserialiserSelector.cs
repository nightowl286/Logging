using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.Thread.Versions;

namespace TNO.Logging.Reading.Entries.Components.Thread;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IThreadComponent"/>.
/// </summary>
internal class ThreadComponentDeserialiserSelector : DeserialiserSelectorBase<IThreadComponent>
{
   public ThreadComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<ThreadComponentDeserialiser0>(0);
   }
}