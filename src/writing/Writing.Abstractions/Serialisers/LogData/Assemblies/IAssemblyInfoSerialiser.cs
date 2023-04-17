using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers.LogData.Assemblies;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.AssemblyInfo)]
public interface IAssemblyInfoSerialiser : IBinarySerialiser<IAssemblyInfo>, IVersioned
{
}