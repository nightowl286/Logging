using System.Runtime.InteropServices;
using TNO.Logging.Common.Exceptions.Abstractions;
using TNO.Logging.Common.Exceptions.Abstractions.System;
using TNO.Logging.Common.Exceptions.System;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Exceptions;
using TNO.Logging.Reading.Deserialisers;

namespace TNO.Logging.Reading.Exceptions.System;

/// <summary>
/// A deserialiser for <see cref="IArgumentOutOfRangeExceptionData"/>.
/// </summary>
[Guid(ExceptionGroups.System.ArgumentOutOfRangeException)]
public sealed class ArgumentOutOfRangeExceptionDataDeserialiser : IExceptionDataDeserialiser<IArgumentOutOfRangeExceptionData>
{
   #region Fields
   private readonly IPrimitiveDeserialiser _primitiveDeserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ArgumentOutOfRangeExceptionDataDeserialiser"/>.</summary>
   /// <param name="primitiveDeserialiser">The <see cref="IPrimitiveDeserialiser"/> to use.</param>
   public ArgumentOutOfRangeExceptionDataDeserialiser(IPrimitiveDeserialiser primitiveDeserialiser)
   {
      _primitiveDeserialiser = primitiveDeserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IArgumentOutOfRangeExceptionData Deserialise(BinaryReader reader)
   {
      string? paramName = reader.TryReadNullable(reader.ReadString);
      object? value = _primitiveDeserialiser.Deserialise(reader);

      return new ArgumentOutOfRangeExceptionData(paramName, value);
   }
   #endregion
}
