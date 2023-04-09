﻿using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.Methods.ConstructorInfos;

/// <inheritdoc/>
public interface IConstructorInfoDeserialiser : IBinaryDeserialiser<IConstructorInfo>, IVersioned
{
}