using TNO.Common.Abstractions.Components;

namespace TNO.Logging.Writer.Loggers.Serialisers.Components;
internal interface IComponentSerialiser<T> : ISerialiser<T> where T : notnull, IEntryComponent
{
}
