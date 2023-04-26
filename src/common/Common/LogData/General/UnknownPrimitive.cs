using TNO.Logging.Common.Abstractions.LogData.General.Primitives;

namespace TNO.Logging.Common.LogData.Tables;

/// <summary>
/// Represents info about an unknown primitive value.
/// </summary>
/// <param name="TypeId">The type id of the <see cref="Type"/> that the value was of.</param>
public record class UnknownPrimitive(ulong TypeId) : IUnknownPrimitive;
