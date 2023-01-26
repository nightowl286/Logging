﻿using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries;
using TNO.Logging.Reading.Deserialisers;

namespace TNO.Logging.Reading.Entries.Versions;

/// <summary>
/// An <see cref="IDeserialiserSelector{T, U}"/> for versions of the <see cref="IEntryDeserialiser"/>.
/// </summary>
internal class EntryDeserialiserSelector : DeserialiserSelectorBase<IEntryDeserialiser, IEntry>
{
   public EntryDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<EntryDeserialiser0>(0);
   }
}
