using TNO.Logging.Common.Abstractions.LogData.Primitives;

namespace TNO.Logging.Common.LogData.Tables;

/// <summary>
/// Represents info about a table.
/// </summary>
public record class CollectionInfo(IReadOnlyCollection<object?> Collection) : ICollectionInfo;