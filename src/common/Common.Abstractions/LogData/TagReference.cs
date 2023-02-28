namespace TNO.Logging.Common.Abstractions.LogData;

/// <summary>
/// Represents a link between a <see cref="Tag"/> and it's <see cref="Id"/>.
/// </summary>
/// <param name="Tag">The tag that the <see cref="Id"/> is associated with.</param>
/// <param name="Id">The id that the <see cref="Tag"/> is associated with.</param>
public record struct TagReference(string Tag, ulong Id);
