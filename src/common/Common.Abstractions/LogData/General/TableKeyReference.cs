using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Abstractions.LogData.Primitives;

/// <summary>
/// Represents a link between a key in a <see cref="ITableComponent"/> and it's <see cref="Id"/>.
/// </summary>
/// <param name="Key">The key that the <see cref="Id"/> is associated with.</param>
/// <param name="Id">The id that the <see cref="Key"/> is associated with.</param>
public record struct TableKeyReference(string Key, uint Id);