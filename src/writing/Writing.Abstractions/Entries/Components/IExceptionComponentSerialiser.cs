﻿using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Entries.Components;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.Exception)]
public interface IExceptionComponentSerialiser : IBinarySerialiser<IExceptionComponent>, IVersioned
{
}