using System.Collections.Generic;
using TNO.Logging.Common.Abstractions.DataKinds;

namespace TNO.Logging.Common.Abstractions;

/// <summary>
/// Represents a map of data kinds and an associated version.
/// </summary>
public class DataVersionMap : Dictionary<VersionedDataKind, uint>
{
}