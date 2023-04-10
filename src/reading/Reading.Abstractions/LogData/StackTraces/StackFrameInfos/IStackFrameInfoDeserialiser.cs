﻿using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackFrameInfos;

/// <inheritdoc/>
public interface IStackFrameInfoDeserialiser : IBinaryDeserialiser<IStackFrameInfo>, IVersioned
{
}