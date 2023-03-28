using System;

namespace TNO.Logging.Common.Abstractions.LogData.Types;

/// <summary>
/// Contains useful extension methods related to the <see cref="TypeIdentity"/>.
/// </summary>
public static class TypeIdentityExtensions
{
   #region Methods
   /// <summary>Gets the <see cref="TypeIdentity"/> of the given <paramref name="type"/>.</summary>
   /// <param name="type">The <see cref="Type"/> to get the identity of.</param>
   /// <returns>The identity of the given <paramref name="type"/>.</returns>
   public static TypeIdentity GetIdentity(this Type type)
      => new TypeIdentity(type);
   #endregion
}
