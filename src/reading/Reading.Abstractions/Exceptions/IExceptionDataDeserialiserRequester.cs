using System;
using System.Diagnostics.CodeAnalysis;

namespace TNO.Logging.Reading.Abstractions.Exceptions;

/// <summary>
/// Denotes a requester for types that implement <see cref="IExceptionDataDeserialiser{TExceptionData}"/>.
/// </summary>
public interface IExceptionDataDeserialiserRequester
{
   #region Methods
   /// <summary>
   /// Tries to get the <paramref name="info"/> for an <see cref="IExceptionDataDeserialiser{TExceptionData}"/>
   /// with the given <paramref name="id"/>.
   /// </summary>
   /// <param name="id">The id of the deserialiser.</param>
   /// <param name="info">
   /// The information about an <see cref="IExceptionDataDeserialiser{TExceptionData}"/>,
   /// or <see langword="null"/> if this method returns <see langword="false"/>.
   /// </param>
   /// <returns>
   /// <see langword="true"/> if the <paramref name="info"/>
   /// could be found, <see langword="false"/> otherwise.
   /// </returns>
   bool ById(Guid id, [NotNullWhen(true)] out IExceptionDataDeserialiserInfo? info);
   #endregion
}
