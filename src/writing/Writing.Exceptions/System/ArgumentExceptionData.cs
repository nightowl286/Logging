using Data = TNO.Logging.Common.Exceptions.System.ArgumentExceptionData;

namespace TNO.Logging.Writing.ArgumentExceptions.System;

[Guid(ExceptionGroups.System.ArgumentException)]
public sealed class ArgumentExceptionData :
   IExceptionDataConverter<ArgumentException, IArgumentExceptionData>,
   IExceptionDataSerialiser<IArgumentExceptionData>
{
   #region Methods
   public IArgumentExceptionData Convert(ArgumentException exception) => new Data(exception.ParamName);
   public void Serialise(BinaryWriter writer, IArgumentExceptionData data)
   {
      string? parameterName = data.ParameterName;

      if (writer.TryWriteNullable(parameterName))
         writer.Write(parameterName);
   }
   public ulong Count(IArgumentExceptionData data)
   {
      return (ulong)(BinaryWriterSizeHelper.StringSize(data.ParameterName) + sizeof(bool));
   }
   #endregion
}
