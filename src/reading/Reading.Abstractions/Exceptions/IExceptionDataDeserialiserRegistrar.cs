using System;

namespace TNO.Logging.Reading.Abstractions.Exceptions;

/// <summary>
/// Denotes a registrar for types that implement the <see cref="IExceptionDataDeserialiser{TExceptionData}"/>.
/// </summary>
public interface IExceptionDataDeserialiserRegistrar
{
   #region Methods
   /// <summary>Registers the given <paramref name="deserialiserType"/>.</summary>
   /// <param name="deserialiserType">The type that implements the <see cref="IExceptionDataDeserialiser{TExceptionData}"/>.</param>
   void Register(Type deserialiserType);
   #endregion
}
