namespace TNO.Logging.Common.Abstractions.LogData.Types;

/// <summary>
/// Represents a link between an <see cref="TypeInfo"/> and it's <see cref="Id"/>.
/// </summary>
/// <param name="TypeInfo">The type info that the <see cref="Id"/> is associated with.</param>
/// <param name="Id">The id that the <see cref="TypeInfo"/> is associated with.</param>
public record struct TypeReference(ITypeInfo TypeInfo, ulong Id);
