using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Entries.Components;
using TNO.Logging.Writing.Serialisers;
using TNO.Logging.Writing.Serialisers.LogData.Methods;

namespace TNO.Logging.Writing.SerialiserProviders;
internal class NonVersionedSerialiserProvider : SerialiserProviderWrapperBase
{
   public NonVersionedSerialiserProvider(IServiceScope scope, ISerialiserProvider? innerProvider = null) : base(scope, innerProvider)
   {
      Scope.Registrar
         .Singleton<IDataVersionMapSerialiser, DataVersionMapSerialiser>()
         .Singleton<IMethodBaseInfoSerialiserDispatcher, MethodBaseInfoSerialiserDispatcher>()
         .Singleton<IComponentSerialiserDispatcher, ComponentSerialiserDispatcher>();
   }
}
