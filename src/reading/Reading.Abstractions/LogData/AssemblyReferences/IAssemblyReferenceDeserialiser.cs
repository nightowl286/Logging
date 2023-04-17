using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.AssemblyReferences;

/// <inheritdoc/>
public interface IAssemblyReferenceDeserialiser : IBinaryDeserialiser<AssemblyReference>, IVersioned
{
}
