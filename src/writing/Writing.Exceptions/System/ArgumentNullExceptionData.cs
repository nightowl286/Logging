using Data = TNO.Logging.Common.Exceptions.System.ArgumentNullExceptionData;

namespace TNO.Logging.Writing.ArgumentExceptions.System;

[Guid(ExceptionGroups.System.ArgumentNullException)]
public sealed class ArgumentNullExceptionData :
   IExceptionDataConverter<ArgumentNullException, IArgumentNullExceptionData>,
   IExceptionDataSerialiser<IArgumentNullExceptionData>
{
   #region Methods
   public IArgumentNullExceptionData Convert(ArgumentNullException exception) => new Data(exception.ParamName);
   public void Serialise(BinaryWriter writer, IArgumentNullExceptionData data)
   {
      string? parameterName = data.ParameterName;

      if (writer.TryWriteNullable(parameterName))
         writer.Write(parameterName);
   }
   public ulong Count(IArgumentNullExceptionData data)
   {
      return (ulong)(BinaryWriterSizeHelper.StringSize(data.ParameterName) + sizeof(bool));
   }
   #endregion
}
