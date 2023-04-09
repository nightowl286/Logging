﻿using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.ConstructorInfos;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.ConstructorInfos.Versions;

namespace TNO.Logging.Reading.LogData.ConstructorInfos;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="IConstructorInfo"/>.
/// </summary>
internal class ConstructorInfoDeserialiserSelector : DeserialiserSelectorBase<IConstructorInfoDeserialiser>, IConstructorInfoDeserialiserSelector
{
   public ConstructorInfoDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<ConstructorInfoDeserialiser0>(0);
   }
}