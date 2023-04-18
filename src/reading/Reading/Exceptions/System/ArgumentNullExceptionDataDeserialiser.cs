using System.Runtime.InteropServices;
using TNO.Logging.Common.Exceptions.Abstractions;
using TNO.Logging.Common.Exceptions.Abstractions.System;
using TNO.Logging.Common.Exceptions.System;
using TNO.Logging.Reading.Abstractions.Exceptions;
using TNO.Logging.Reading.Deserialisers;

namespace TNO.Logging.Reading.Exceptions.System;

/// <summary>
/// A deserialiser for <see cref="IArgumentNullExceptionData"/>.
/// </summary>
[Guid(ExceptionGroups.System.ArgumentNullException)]
public sealed class ArgumentNullExceptionDataDeserialiser : IExceptionDataDeserialiser<IArgumentNullExceptionData>
{
   #region Methods
   /// <inheritdoc/>
   public IArgumentNullExceptionData Deserialise(BinaryReader reader)
   {
      string? parameterName = reader.TryReadNullable(reader.ReadString);

      return new ArgumentNullExceptionData(parameterName);
   }
   #endregion
}
