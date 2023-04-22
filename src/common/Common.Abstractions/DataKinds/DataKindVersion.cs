namespace TNO.Logging.Common.Abstractions.DataKinds;

/// <summary>
/// Represents a link between a <see cref="DataKind"/> and a <see cref="Version"/>.
/// </summary>
/// <param name="DataKind">The <see cref="VersionedDataKind"/> that represents the versioned data.</param>
/// <param name="Version">The version of the data.</param>
public record struct DataKindVersion(VersionedDataKind DataKind, uint Version);
