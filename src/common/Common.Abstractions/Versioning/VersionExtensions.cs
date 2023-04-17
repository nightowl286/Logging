using System;
using System.Reflection;

namespace TNO.Logging.Common.Abstractions.Versioning;

/// <summary>
/// Contains useful extension methods related to the <see cref="IVersioned"/>
/// interface, and the <see cref="VersionAttribute"/>.
/// </summary>
public static class VersionExtensions
{
   #region Methods
   /// <summary>
   /// Tries to extract the version specified in a <see cref="VersionAttribute"/>,
   ///  on the given <paramref name="type"/>.
   /// </summary>
   /// <param name="type">The type to get the version of.</param>
   /// <param name="version">The extracted version.</param>
   /// <returns>
   /// <see langword="true"/> if the given type had a <see cref="VersionAttribute"/>,
   /// <see langword="false"/> otherwise.
   /// </returns>
   public static bool TryGetVersion(this Type type, out uint version)
   {
      VersionAttribute? attr = type.GetCustomAttribute<VersionAttribute>();
      if (attr is null)
      {
         version = default;
         return false;
      }

      version = attr.Version;
      return true;
   }

   /// <summary>
   /// Extracts the version specified in a <see cref="VersionAttribute"/>,
   /// on the given <paramref name="type"/>.
   /// </summary>
   /// <param name="type">The type to get the version of.</param>
   /// <returns>The extracted version.</returns>
   /// <exception cref="ArgumentException">Thrown if the <paramref name="type"/> does not have the <see cref="VersionAttribute"/>.</exception>
   public static uint GetVersion(this Type type)
   {
      if (TryGetVersion(type, out uint version))
         return version;

      throw new ArgumentException($"The given type ({type}) did not have the {typeof(VersionAttribute)}.");
   }

   /// <summary>
   /// Extracts the version specified in a <see cref="VersionAttribute"/>,
   /// on the type of the given <paramref name="instance"/>.
   /// </summary>
   /// <param name="instance">The versioned instance to get the type version of.</param>
   /// <returns>The extracted version.</returns>
   /// <exception cref="ArgumentException">Thrown if the type of the <paramref name="instance"/> does not have the <see cref="VersionAttribute"/>.</exception>
   public static uint GetVersion(this IVersioned instance) => GetVersion(instance.GetType());
   #endregion
}
