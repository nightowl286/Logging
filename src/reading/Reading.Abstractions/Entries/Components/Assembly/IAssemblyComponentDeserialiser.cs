﻿using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Entries.Components.Assembly;


/// <inheritdoc/>
public interface IAssemblyComponentDeserialiser : IBinaryDeserialiser<IAssemblyComponent>, IVersioned
{
}