using TNO.Logging.Common.Exceptions.Abstractions.System;

namespace TNO.Logging.Common.Exceptions.System;

/// <summary>
/// Represents the custom data related to an <see cref="ArgumentNullException"/>.
/// </summary>
public record class ArgumentNullExceptionData(string? ParameterName) : ArgumentExceptionData(ParameterName), IArgumentNullExceptionData;
