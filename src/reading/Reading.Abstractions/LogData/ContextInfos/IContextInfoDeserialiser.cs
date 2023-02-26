﻿using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.ContextInfos;

/// <inheritdoc/>
public interface IContextInfoDeserialiser : IBinaryDeserialiser<ContextInfo>, IVersioned
{
}