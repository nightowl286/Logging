﻿using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers.LogData.Types;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.TypeReference)]
public interface ITypeReferenceSerialiser : IBinarySerialiser<TypeReference>, IVersioned
{
}