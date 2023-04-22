using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Serialisers;
using TNO.Logging.Writing.Serialisers.Registrants;

namespace TNO.Writing.Tests;

internal static class GeneralSerialiser
{
   #region Properties
   public static ISerialiser Instance { get; } = GetSerialiser();
   #endregion

   #region Functions
   public static ISerialiser GetSerialiser()
   {
      IServiceScope scope = new ServiceFacade().CreateNew();
      IServiceRegistrar registrar = scope.Registrar;

      Serialiser serialiser = new Serialiser(scope.Requester);

      registrar.Instance<ISerialiser>(serialiser);

      new BuiltinSerialiserRegistrant().Register(scope);

      return serialiser;
   }
   #endregion
}
