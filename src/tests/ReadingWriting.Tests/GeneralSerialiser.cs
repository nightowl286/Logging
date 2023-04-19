using System.Reflection;
using TNO.Common.Extensions;
using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Serialisers;

namespace TNO.ReadingWriting.Tests;

internal static class GeneralSerialiser
{
   #region Properties
   public static ISerialiser Instance { get; } = GetSerialiser();
   #endregion

   #region Functions
   private static ISerialiser GetSerialiser()
   {
      IServiceScope scope = new ServiceFacade().CreateNew();
      IServiceRegistrar registrar = scope.Registrar;

      Serialiser serialiser = new Serialiser(scope.Requester);

      registrar.Instance<ISerialiser>(serialiser);

      foreach ((Type service, Type concrete) in GetSerialisersTypes())
         registrar.Singleton(service, concrete);

      return serialiser;
   }
   private static IEnumerable<(Type, Type)> GetSerialisersTypes()
   {
      Assembly assembly = Assembly.Load("TNO.Logging.Writing");
      Type[] allTypes = assembly.GetTypes();

      foreach (Type type in allTypes)
      {
         IEnumerable<Type> implementations = type.GetOpenInterfaceImplementations(typeof(ISerialiser<>));
         foreach (Type implementation in implementations)
            yield return (implementation, type);
      }
   }
   #endregion
}
