using System.Runtime.InteropServices;
using TNO.Logging.Common.Exceptions.Abstractions;
using TNO.Logging.Common.Exceptions.Abstractions.System;
using TNO.Logging.Writing.Abstractions.Exceptions;
using TNO.Logging.Writing.Serialisers;
using Data = TNO.Logging.Common.Exceptions.System.ArgumentExceptionData;

namespace TNO.Logging.Writing.Exceptions.System;

/// <summary>
/// Converts and serialises exceptions of the <see cref="ArgumentException"/> type.
/// </summary>
[Guid(ExceptionGroups.System.ArgumentException)]
public sealed class ArgumentExceptionHandler : IExceptionDataHandler<ArgumentException, IArgumentExceptionData>
{
   #region Methods
   /// <inheritdoc/>
   public IArgumentExceptionData Convert(ArgumentException exception) => new Data(exception.ParamName);

   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IArgumentExceptionData data)
   {
      string? parameterName = data.ParameterName;

      if (writer.TryWriteNullable(parameterName))
         writer.Write(parameterName);
   }

   /// <inheritdoc/>
   public ulong Count(IArgumentExceptionData data)
   {
      return (ulong)(BinaryWriterSizeHelper.StringSize(data.ParameterName) + sizeof(bool));
   }
   #endregion
}
