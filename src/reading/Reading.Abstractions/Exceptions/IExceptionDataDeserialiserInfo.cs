using System;

namespace TNO.Logging.Reading.Abstractions.Exceptions;

/// <summary>
/// Denotes information about an implementation of the <see cref="IExceptionDataDeserialiser{TExceptionData}"/>.
/// </summary>
public interface IExceptionDataDeserialiserInfo
{
   #region Properties
   /// <summary>The id of the deserialiser.</summary>
   Guid Id { get; }

   /// <summary>The type of the deserialiser itself.</summary>
   Type DeserialiserType { get; }
   #endregion
}
