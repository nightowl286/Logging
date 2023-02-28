namespace TNO.Logging.Common.Abstractions.LogData;

/// <summary>
/// Represents the info about a given logging context.
/// </summary>
/// <param name="Name">
/// The name that was given to this context, it is not guaranteed to be unique.
/// </param>
/// <param name="Id">The id of this context.</param>
/// <param name="ParentId">The id of this context's parent.</param>
/// <param name="FileId">The id of the file where this context has been created.</param>
/// <param name="LineInFile">
/// The line number in the file (specified by the <see cref="FileId"/>)
/// where this context has been created.
/// </param>
public record struct ContextInfo(string Name, ulong Id, ulong ParentId, ulong FileId, uint LineInFile);
