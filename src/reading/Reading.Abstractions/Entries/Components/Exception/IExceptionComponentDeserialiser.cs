﻿using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.Entries.Components.Exception;


/// <inheritdoc/>
public interface IExceptionComponentDeserialiser : IBinaryDeserialiser<IExceptionComponent>, IVersioned
{
}