using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers.LogData.Tables;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.TableInfo)]
public interface ITableInfoSerialiser : IBinarySerialiser<ITableInfo>, IVersioned
{
}
