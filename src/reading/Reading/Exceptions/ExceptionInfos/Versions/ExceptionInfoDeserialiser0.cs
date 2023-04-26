using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Exceptions;
using TNO.Logging.Reading.Deserialisers;

namespace TNO.Logging.Reading.Exceptions.ExceptionInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IExceptionInfo"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.ExceptionInfo)]
public sealed class ExceptionInfoDeserialiser0 : IDeserialiser<IExceptionInfo>
{
   #region Fields
   private readonly IExceptionDataDeserialiser _exceptionDataDeserialiser;
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionInfoDeserialiser0"/>.</summary>
   /// <param name="exceptionDataDeserialiser">The <see cref="IExceptionDataDeserialiser"/> to use.</param>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>
   public ExceptionInfoDeserialiser0(
      IExceptionDataDeserialiser exceptionDataDeserialiser,
      IDeserialiser deserialiser)
   {
      _exceptionDataDeserialiser = exceptionDataDeserialiser;
      _deserialiser = deserialiser;
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

      _deserialiser.Deserialise(reader, out IStackTraceInfo stackTrace);

      bool hasAdditionalData = reader.ReadBoolean();
      ITableInfo? additionalData = hasAdditionalData ? _deserialiser.Deserialise<ITableInfo>(reader) : null;

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
