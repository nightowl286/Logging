using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Serialisers;

namespace TNO.Logging.Writing.SerialiserProviders;
internal class NonVersionedSerialiserProvider : SerialiserProviderWrapperBase
{
   public NonVersionedSerialiserProvider(IServiceScope scope, ISerialiserProvider? innerProvider = null) : base(scope, innerProvider)
   {
      Scope.Registrar
         .Singleton<ISerialiser<DataVersionMap>, DataVersionMapSerialiser>()
         .Instance<ISerialiser>(new Serialiser(scope.Requester));
   }
}
