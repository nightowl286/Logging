using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.LogData.General;
using TNO.Logging.Reading.LogData.Methods;

namespace TNO.Logging.Reading.Deserialisers.Registrants;

/// <summary>
/// Represents an <see cref="IDeserialiserRegistrant"/> that will register the non-versioned built-in deserialisers.
/// </summary>
public sealed class BuiltinDeserialiserRegistrant : IDeserialiserRegistrant
{
   #region Methods
   /// <inheritdoc/>
   public void Register(IServiceScope scope)
   {
      scope.Registrar
         .Singleton<IDeserialiser<DataVersionMap>, DataVersionMapDeserialiser>()
         .Singleton<IDeserialiser<DataKindVersion>, DataVersionMapDeserialiser>()
         .Singleton<IDeserialiser<IMethodBaseInfo>, MethodBaseInfoDeserialiserDispatcher>()
         .Singleton<IDeserialiser<ITableInfo>, TableInfoDeserialiser>()
         .Singleton<IDeserialiser<ICollectionInfo>, CollectionInfoDeserialiser>()
         .Singleton<IPrimitiveDeserialiser, PrimitiveDeserialiser>();
   }
   #endregion
}
