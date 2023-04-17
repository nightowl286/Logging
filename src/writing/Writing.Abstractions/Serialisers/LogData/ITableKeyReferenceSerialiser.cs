using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers.LogData;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.TableKeyReference)]
public interface ITableKeyReferenceSerialiser : IBinarySerialiser<TableKeyReference>, IVersioned
{
}