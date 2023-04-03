using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Abstractions.Serialisers.LogData.Assemblies;

/// <inheritdoc/>
[VersionedDataKind(VersionedDataKind.AssemblyReference)]
public interface IAssemblyReferenceSerialiser : IBinarySerialiser<AssemblyReference>, IVersioned
{
}