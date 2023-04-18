using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Exceptions;
using TNO.Logging.Reading.Abstractions.Exceptions.ExceptionInfos;
using TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackTraceInfos;
using TNO.Logging.Reading.Abstractions.LogData.Tables;
using TNO.Logging.Reading.Deserialisers;

namespace TNO.Logging.Reading.Exceptions.ExceptionInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IExceptionInfo"/>, version #0.
/// </summary>
[Version(0)]
public sealed class ExceptionInfoDeserialiser0 : IExceptionInfoDeserialiser
{
   #region Fields
   private readonly IExceptionDataDeserialiser _exceptionDataDeserialiser;
   private readonly IStackTraceInfoDeserialiser _stackTraceInfoDeserialiser;
   private readonly ITableInfoDeserialiser _tableInfoDeserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionInfoDeserialiser0"/>.</summary>
   /// <param name="exceptionDataDeserialiser">The <see cref="IExceptionDataDeserialiser"/> to use.</param>
   /// <param name="stackTraceInfoDeserialiser">The <see cref="IStackTraceInfoDeserialiser"/> to use.</param>
   /// <param name="tableInfoDeserialiser">The <see cref="ITableInfoDeserialiser"/> to use.</param>
   public ExceptionInfoDeserialiser0(
      IExceptionDataDeserialiser exceptionDataDeserialiser,
      IStackTraceInfoDeserialiser stackTraceInfoDeserialiser,
      ITableInfoDeserialiser tableInfoDeserialiser)
   {
      _exceptionDataDeserialiser = exceptionDataDeserialiser;
      _stackTraceInfoDeserialiser = stackTraceInfoDeserialiser;
      _tableInfoDeserialiser = tableInfoDeserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IExceptionInfo Deserialise(BinaryReader reader)
   {
      ulong exceptionTypeId = reader.ReadUInt64();
      ulong exceptionDataTypeId = reader.ReadUInt64();
      Guid exceptionGroupId = reader.ReadGuid();

      string message = reader.ReadString();
      IStackTraceInfo stackTrace = _stackTraceInfoDeserialiser.Deserialise(reader);
      ITableInfo additionalData = _tableInfoDeserialiser.Deserialise(reader);

      IExceptionData data = _exceptionDataDeserialiser.Deserialise(reader, exceptionGroupId);

      IExceptionInfo? innerException = reader.TryReadNullable(() => Deserialise(reader));

      return ExceptionInfoFactory.Version0(
         exceptionTypeId,
         exceptionDataTypeId,
         exceptionGroupId,
         message,
         stackTrace,
         additionalData,
         data,
         innerException);
   }
   #endregion
}
