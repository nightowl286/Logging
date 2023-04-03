using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.AssemblyReferences;

/// <inheritdoc/>
public interface IAssemblyReferenceDeserialiser : IBinaryDeserialiser<AssemblyReference>, IVersioned
{
}
