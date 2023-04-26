using TNO.Logging.Reading.Abstractions.Exceptions;

namespace TNO.Logging.Reading.Exceptions;

/// <summary>
/// Represents information about an implementation of <see cref="IExceptionDataDeserialiser{TExceptionData}"/>.
/// </summary>
public class ExceptionDataDeserialiserInfo : IExceptionDataDeserialiserInfo
{
   #region Properties
   /// <inheritdoc/>
   public Guid Id { get; }

   /// <inheritdoc/>
   public Type DeserialiserType { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionDataDeserialiserInfo"/>.</summary>
   /// <param name="id">The id of the deserialiser.</param>
   /// <param name="deserialiserType">The type of the deserialiser itself.</param>
   public ExceptionDataDeserialiserInfo(Guid id, Type deserialiserType)
   {
      Id = id;
      DeserialiserType = deserialiserType;
   }
   #endregion
}
