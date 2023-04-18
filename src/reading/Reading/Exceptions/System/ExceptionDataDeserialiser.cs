using System.Runtime.InteropServices;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Exceptions.Abstractions;
using TNO.Logging.Common.Exceptions.System;
using TNO.Logging.Reading.Abstractions.Exceptions;

namespace TNO.Logging.Reading.Exceptions.System;

/// <summary>
/// A deserialiser for <see cref="IExceptionData"/>.
/// </summary>
[Guid(ExceptionGroups.System.Exception)]
public sealed class ExceptionDataDeserialiser : IExceptionDataDeserialiser<IExceptionData>
{
   #region Methods
   /// <inheritdoc/>
   public IExceptionData Deserialise(BinaryReader reader) => ExceptionData.Empty;
   #endregion
}
