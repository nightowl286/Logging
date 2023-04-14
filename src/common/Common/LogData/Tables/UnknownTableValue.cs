using TNO.Logging.Common.Abstractions.LogData.Tables;

namespace TNO.Logging.Common.LogData.Tables;

/// <summary>
/// Represents info about an unknown table value.
/// </summary>
/// <param name="TypeId">The type of the <see cref="Type"/> that the value was of.</param>
public record class UnknownTableValue(ulong TypeId) : IUnknownTableValue;
