﻿using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.Components.Tag;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.Tag.Versions;

namespace TNO.Logging.Reading.Entries.Components.Tag;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="ITagComponentDeserialiser"/>.
/// </summary>
internal class TagComponentDeserialiserSelector : DeserialiserSelectorBase<ITagComponentDeserialiser>, ITagComponentDeserialiserSelector
{
   public TagComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<TagComponentDeserialiser0>(0);
   }
}