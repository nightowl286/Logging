using System.Runtime.InteropServices;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Exceptions.Abstractions;
using TNO.Logging.Common.Exceptions.System;
using TNO.Logging.Writing.Abstractions.Exceptions;

namespace TNO.Logging.Writing.Exceptions.System;

/// <summary>
/// Converts and serialises exceptions of the <see cref="Exception"/> type.
/// </summary>
[Guid(ExceptionGroups.System.Exception)]
public sealed class ExceptionHandler : IExceptionDataHandler<Exception, IExceptionData>
{
   #region Methods
   /// <inheritdoc/>
   public IExceptionData Convert(Exception exception) => ExceptionData.Empty;

   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IExceptionData data) { }

   /// <inheritdoc/>
   public int Count(IExceptionData data) => 0;
   #endregion
}
