using System.Runtime.InteropServices;
using TNO.Logging.Common.Exceptions.Abstractions;
using TNO.Logging.Common.Exceptions.Abstractions.System;
using TNO.Logging.Common.Exceptions.System;
using TNO.Logging.Reading.Abstractions.Exceptions;
using TNO.Logging.Reading.Deserialisers;

namespace TNO.Logging.Reading.Exceptions.System;

/// <summary>
/// A deserialiser for <see cref="IArgumentExceptionData"/>.
/// </summary>
[Guid(ExceptionGroups.System.ArgumentException)]
public sealed class ArgumentExceptionDataDeserialiser : IExceptionDataDeserialiser<IArgumentExceptionData>
{
   #region Methods
   /// <inheritdoc/>
   public IArgumentExceptionData Deserialise(BinaryReader reader)
   {
      string? parameterName = reader.TryReadNullable(reader.ReadString);

      return new ArgumentExceptionData(parameterName);
   }
   #endregion
}
