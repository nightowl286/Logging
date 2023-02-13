using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TNO.Logging.Common.Abstractions.DataKinds;

/// <summary>
/// Contains useful extensions related to the <see cref="VersionedDataKind"/> enum.
/// </summary>
public static class VersionedDataKindExtensions
{
   #region Methods
   /// <summary>Gets all the data kinds associated with the given <paramref name="type"/>, and it's interfaces.</summary>
   /// <param name="type">The type to check for data kinds.</param>
   /// <returns>An enumeration of the associated data kinds.</returns>
   public static IEnumerable<VersionedDataKind> GetDataKinds(this Type type)
   {
      IEnumerable<VersionedDataKindAttribute> attributes = type.GetCustomAttributes<VersionedDataKindAttribute>();
      IEnumerable<VersionedDataKindAttribute> interfaceAttributes = type
            .GetInterfaces()
            .SelectMany(i => i.GetCustomAttributes<VersionedDataKindAttribute>());


      attributes = attributes.Concat(interfaceAttributes);
      foreach (VersionedDataKindAttribute attribute in attributes)
         yield return attribute.Kind;
   }
   #endregion
}