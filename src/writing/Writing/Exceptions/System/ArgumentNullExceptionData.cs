using System.Runtime.InteropServices;
using TNO.Logging.Common.Exceptions.Abstractions;
using TNO.Logging.Common.Exceptions.Abstractions.System;
using TNO.Logging.Writing.Abstractions.Exceptions;
using TNO.Logging.Writing.Serialisers;
using Data = TNO.Logging.Common.Exceptions.System.ArgumentNullExceptionData;

namespace TNO.Logging.Writing.Exceptions.System;

/// <summary>
/// Converts and serialises exceptions of the <see cref="ArgumentNullException"/> type.
/// </summary>
[Guid(ExceptionGroups.System.ArgumentNullException)]
public sealed class ArgumentNullExceptionData :
   IExceptionDataConverter<ArgumentNullException, IArgumentNullExceptionData>,
   IExceptionDataSerialiser<IArgumentNullExceptionData>
{
   #region Methods
   /// <inheritdoc/>
   public IArgumentNullExceptionData Convert(ArgumentNullException exception) => new Data(exception.ParamName);

   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IArgumentNullExceptionData data)
   {
      string? parameterName = data.ParameterName;

      if (writer.TryWriteNullable(parameterName))
         writer.Write(parameterName);
   }

   /// <inheritdoc/>
   public ulong Count(IArgumentNullExceptionData data)
   {
      return (ulong)(BinaryWriterSizeHelper.StringSize(data.ParameterName) + sizeof(bool));
   }
   #endregion
}
