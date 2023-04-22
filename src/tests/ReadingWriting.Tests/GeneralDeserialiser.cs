using System.Reflection;
using TNO.Common.Extensions;
using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Deserialisers.Registrants;

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

      DataVersionMap latestMap = GetLatestVersionMap();

      new BuiltinDeserialiserRegistrant().Register(scope);
      new BuiltInVersionMapDeserialiserRegistrant(latestMap).Register(scope);

      return deserialiser;
   }

   private static DataVersionMap GetLatestVersionMap()
   {
      Dictionary<VersionedDataKind, uint> versions = new Dictionary<VersionedDataKind, uint>();

      Assembly assembly = Assembly.Load("TNO.Logging.Reading");
      Type[] allTypes = assembly.GetTypes();

      foreach (Type type in allTypes)
      {
         if (type.ImplementsOpenInterface(typeof(IDeserialiser<>)) == false)
            continue;

         if (type.TryGetVersion(out uint version) == false)
            continue;

         VersionedDataKindAttribute? dataKindAttribute = type.GetCustomAttribute<VersionedDataKindAttribute>();

         if (dataKindAttribute is null)
            continue;

         VersionedDataKind dataKind = dataKindAttribute.Kind;

         if (versions.TryGetValue(dataKind, out uint currentVersion) == false || currentVersion < version)
            versions[dataKind] = version;
      }

      DataVersionMap map = new DataVersionMap();
      foreach (KeyValuePair<VersionedDataKind, uint> pair in versions)
      {
         DataKindVersion dataKindVersion = new DataKindVersion(pair.Key, pair.Value);
         map.Add(dataKindVersion);
      }

      return map;
   }
   #endregion
}
