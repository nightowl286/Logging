using System.Runtime.InteropServices;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Exceptions.Abstractions;
using TNO.Logging.Writing.Abstractions.Exceptions;
using Data = TNO.Logging.Common.Exceptions.System.ExceptionData;

namespace TNO.Logging.Writing.Exceptions.System;

/// <summary>
/// Converts and serialises exceptions of the <see cref="Exception"/> type.
/// </summary>
[Guid(ExceptionGroups.System.Exception)]
public sealed class ExceptionData :
   IExceptionDataConverter<Exception, IExceptionData>,
   IExceptionDataSerialiser<IExceptionData>
{
   #region Methods
   /// <inheritdoc/>
   public IExceptionData Convert(Exception exception) => Data.Empty;

   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IExceptionData data) { }

   /// <inheritdoc/>
   public ulong Count(IExceptionData data) => 0;
   #endregion
}
