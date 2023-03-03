using System.Reflection;

namespace TNO.Logging.Common.Abstractions.LogData.Assemblies;

/// <summary>
/// Contains useful extension methods related to the <see cref="AssemblyIdentity"/>.
/// </summary>
public static class AssemblyIdentitiyExtensions
{
   #region Methods
   /// <summary>Gets the <see cref="AssemblyIdentity"/> of the given <paramref name="assembly"/>.</summary>
   /// <param name="assembly">The <see cref="Assembly"/> to get the identity of.</param>
   /// <returns>The identity of the given <paramref name="assembly"/>.</returns>
   public static AssemblyIdentity GetIdentity(this Assembly assembly)
      => new AssemblyIdentity(assembly);
   #endregion
}
