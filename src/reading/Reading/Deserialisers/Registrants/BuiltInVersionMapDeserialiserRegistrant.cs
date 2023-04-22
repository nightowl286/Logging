using System.Reflection;
using TNO.Common.Extensions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Deserialisers.Registrants;

/// <summary>
/// Represents an <see cref="IDeserialiserRegistrant"/> that will register the versioned built-in deserialisers.
/// </summary>
public sealed class BuiltInVersionMapDeserialiserRegistrant : IDeserialiserRegistrant
{
   #region Subclass
   private record struct DeserialiserInfo(Type DeserialiserType, Type ServiceType);
   #endregion

   #region Fields
   private readonly DataVersionMap _versionMap;
   #endregion

   #region Constructors
   /// <summary>Creates a new <see cref="BuiltinDeserialiserRegistrant"/> for the given <paramref name="versionMap"/>.</summary>
   /// <param name="versionMap">The version map that will determine the versions of the registered deserialisers.</param>
   public BuiltInVersionMapDeserialiserRegistrant(DataVersionMap versionMap) => _versionMap = versionMap;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Register(IServiceScope scope)
   {
      Dictionary<DataKindVersion, DeserialiserInfo> deserialiserVersions = GetDeserialiserVersions();

      foreach (DataKindVersion dataKindVersion in _versionMap)
      {
         if (deserialiserVersions.TryGetValue(dataKindVersion, out DeserialiserInfo deserialiserInfo) == false)
            throw new Exception($"Could not find a deserialiser for the type {dataKindVersion.DataKind}, #{dataKindVersion.Version}.");

         scope.Registrar.Singleton(deserialiserInfo.ServiceType, deserialiserInfo.DeserialiserType);
      }
   }

   private static Dictionary<DataKindVersion, DeserialiserInfo> GetDeserialiserVersions()
   {
      Dictionary<DataKindVersion, DeserialiserInfo> deserialiserVersions = new Dictionary<DataKindVersion, DeserialiserInfo>();

      Assembly assembly = Assembly.GetExecutingAssembly();
      Type[] allTypes = assembly.GetTypes();
      foreach (Type type in allTypes)
      {
         List<Type> implementations = type.GetOpenInterfaceImplementations(typeof(IDeserialiser<>)).ToList();
         if (implementations.Count != 1)
            continue;

         VersionAttribute? versionAttribute = type.GetCustomAttribute<VersionAttribute>();
         VersionedDataKindAttribute? dataKindAttribute = type.GetCustomAttribute<VersionedDataKindAttribute>();

         if (versionAttribute is null || dataKindAttribute is null) continue;

         uint version = versionAttribute.Version;
         VersionedDataKind dataKind = dataKindAttribute.Kind;
         DataKindVersion dataKindVersion = new DataKindVersion(dataKind, version);

         DeserialiserInfo info = new DeserialiserInfo(type, implementations[0]);
         deserialiserVersions.Add(dataKindVersion, info);
      }

      return deserialiserVersions;
   }
   #endregion
}
