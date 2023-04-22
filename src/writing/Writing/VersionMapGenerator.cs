using System.Reflection;
using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing;

/// <summary>
/// A generator for <see cref="DataVersionMap"/>.
/// </summary>
public static class VersionMapGenerator
{
   #region Functions
   /// <summary>Generates a <see cref="DataVersionMap"/> for the latest versions of <see cref="ISerialiser{T}"/>.</summary>
   /// <returns>The generated <see cref="DataVersionMap"/>.</returns>
   public static DataVersionMap GetForLatestSerialisers()
   {
      DataVersionMap map = new DataVersionMap();

      Assembly assembly = Assembly.GetExecutingAssembly();

      Type[] allTypes = assembly.GetTypes();
      foreach (Type type in allTypes)
      {
         if (type.ImplementsOpenInterface(typeof(ISerialiser<>)) == false)
            continue;

         VersionedDataKindAttribute? dataKindAttribute = type.GetCustomAttribute<VersionedDataKindAttribute>();
         if (dataKindAttribute is null)
            continue;

         uint version = type.GetVersion();
         map.Add(new DataKindVersion(dataKindAttribute.Kind, version));
      }

      return map;
   }
   #endregion
}
