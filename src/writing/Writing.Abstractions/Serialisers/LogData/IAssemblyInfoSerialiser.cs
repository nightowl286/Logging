using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers.LogData;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.AssemblyInfo)]
public interface IAssemblyInfoSerialiser : IBinarySerialiser<IAssemblyInfo>, IVersioned
{
}