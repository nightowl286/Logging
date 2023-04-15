using TNO.Logging.Common.Exceptions.Abstractions.System;

namespace TNO.Logging.Common.Exceptions.System;

/// <summary>
/// Represents the custom data related to an <see cref="ArgumentException"/>.
/// </summary>
public record class ArgumentExceptionData(string? ParameterName) : ExceptionData, IArgumentExceptionData;
