using System.Collections.Generic;

namespace TNO.Logging.Common.Abstractions;

/// <summary>
/// Represents a map of data kinds and an associated version.
/// </summary>
public class DataVersionMap : Dictionary<VersionedDataKind, uint>
{
}
