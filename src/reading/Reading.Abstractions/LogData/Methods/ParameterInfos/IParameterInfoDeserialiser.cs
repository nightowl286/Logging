using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.Methods.ParameterInfos;

/// <inheritdoc/>
public interface IParameterInfoDeserialiser : IBinaryDeserialiser<IParameterInfo>, IVersioned
{
}
