using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Common.Exceptions;

/// <summary>
/// Represents that the <see cref="IExceptionData"/> came from an unknown exception group.
/// </summary>
/// <param name="ExceptionGroupId">The <see cref="Guid"/> that represents the exception group.</param>
public sealed record class UnknownExceptionGroupData(Guid ExceptionGroupId) : IExceptionData;
