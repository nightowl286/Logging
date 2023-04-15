using Data = TNO.Logging.Common.Exceptions.System.ExceptionData;

namespace TNO.Logging.Writing.Exceptions.System;

[Guid(ExceptionGroups.System.Exception)]
public sealed class ExceptionData :
   IExceptionDataConverter<Exception, IExceptionData>,
   IExceptionDataSerialiser<IExceptionData>
{
   #region Methods
   public IExceptionData Convert(Exception exception) => Data.Empty;
   public void Serialise(BinaryWriter writer, IExceptionData data) { }
   public ulong Count(IExceptionData data) => 0;
   #endregion
}
