﻿using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.TypeReferences;

/// <inheritdoc/>
public interface ITypeReferenceDeserialiser : IBinaryDeserialiser<TypeReference>, IVersioned
{
}