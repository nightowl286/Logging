namespace TNO.Logging.Common.Abstractions.LogData;

/// <summary>
/// Represents a link between a <see cref="File"/> and it's <see cref="Id"/>.
/// </summary>
/// <param name="File">The file that the <see cref="Id"/> is associated with.</param>
/// <param name="Id">The id that the <see cref="File"/> is associated with.</param>
public record struct FileReference(string File, ulong Id);