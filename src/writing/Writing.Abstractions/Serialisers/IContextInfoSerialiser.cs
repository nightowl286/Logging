﻿using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.ContextInfo)]
public interface IContextInfoSerialiser : IBinarySerialiser<ContextInfo>, IVersioned
{
}
