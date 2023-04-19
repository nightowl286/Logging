using System.Reflection;
using TNO.Common.Extensions;
using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;

namespace TNO.ReadingWriting.Tests;
internal static class GeneralDeserialiser
{
   #region Properties
   public static IDeserialiser Instance { get; } = GetDeserialiser();
   #endregion

   #region Functions
   private static IDeserialiser GetDeserialiser()
   {
      IServiceScope scope = new ServiceFacade().CreateNew();
      IServiceRegistrar registrar = scope.Registrar;

      Deserialiser deserialiser = new Deserialiser(scope.Requester);

      registrar.Instance<IDeserialiser>(deserialiser);

      foreach ((Type service, Type latest) in GetLatestDeserialisers())
         registrar.Singleton(service, latest);

      return deserialiser;
   }

   private static IEnumerable<(Type, Type)> GetLatestDeserialisers()
   {
      Dictionary<Type, List<Type>> deserialiserVersions = new Dictionary<Type, List<Type>>();

      Assembly assembly = Assembly.Load("TNO.Logging.Reading");
      Type[] allTypes = assembly.GetTypes();

      foreach (Type type in allTypes)
      {
         IEnumerable<Type> implementations = type.GetOpenInterfaceImplementations(typeof(IDeserialiser<>));
         foreach (Type implementation in implementations)
         {
            if (deserialiserVersions.TryGetValue(implementation, out List<Type>? versions) == false)
            {
               versions = new List<Type>();
               deserialiserVersions.Add(implementation, versions);
            }

            versions.Add(type);
         }
      }


      foreach (KeyValuePair<Type, List<Type>> pair in deserialiserVersions)
      {
         Type latestVersion = pair
            .Value
            .OrderByDescending(m => m.GetCustomAttribute<VersionAttribute>(false)?.Version)
            .First();

         yield return (pair.Key, latestVersion);
      }
   }
   #endregion
}
