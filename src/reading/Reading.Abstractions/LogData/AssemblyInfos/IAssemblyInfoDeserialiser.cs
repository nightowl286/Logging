using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.AssemblyInfos;

/// <inheritdoc/>
public interface IAssemblyInfoDeserialiser : IBinaryDeserialiser<IAssemblyInfo>, IVersioned
{
}
