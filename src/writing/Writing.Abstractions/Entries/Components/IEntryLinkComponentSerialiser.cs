﻿using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Entries.Components;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.EntryLink)]
public interface IEntryLinkComponentSerialiser : IBinarySerialiser<IEntryLinkComponent>, IVersioned
{
}
