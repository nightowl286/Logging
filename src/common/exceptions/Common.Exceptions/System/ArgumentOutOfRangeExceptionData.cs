using TNO.Logging.Common.Exceptions.Abstractions.System;

namespace TNO.Logging.Common.Exceptions.System;

/// <summary>
/// Represents the custom data related to an <see cref="ArgumentNullException"/>.
/// </summary>
public record class ArgumentOutOfRangeExceptionData(string? ParameterName, object? ActualValue) : ArgumentExceptionData(ParameterName), IArgumentOutOfRangeExceptionData;
