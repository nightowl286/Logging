using TNO.Logging.Reading.Abstractions.Exceptions;

namespace TNO.Logging.Reading.Exceptions;

/// <summary>
/// Represents information about an implementation of <see cref="IExceptionDataDeserialiser{TExceptionData}"/>.
/// </summary>
/// <param name="Id">The id of the deserialiser.</param>
/// <param name="DeserialiserType">The type of the deserialiser itself.</param>
public record class ExceptionDataDeserialiserInfo(
   Guid Id,
   Type DeserialiserType) : IExceptionDataDeserialiserInfo;
