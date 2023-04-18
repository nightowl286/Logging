using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.ExceptionInfo)]
public interface IExceptionInfoSerialiser : IBinarySerialiser<IExceptionInfo>, IVersioned
{
}
