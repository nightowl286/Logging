using System;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;

namespace TNO.Logging.Common.Abstractions.LogData.Types;

/// <summary>
/// Represents the identity of a <see cref="Type"/>.
/// </summary>
public readonly struct TypeIdentity : IEquatable<TypeIdentity>
{
   #region Fields
   private readonly AssemblyIdentity _assemblyIdentity;
   private readonly string? _fullName;
   private readonly string _name;
   #endregion

   #region Constructors
   /// <summary>Gets the identity of the given <paramref name="type"/>.</summary>
   /// <param name="type">The type to get the identity of.</param>
   public TypeIdentity(Type type)
   {
      _assemblyIdentity = new AssemblyIdentity(type.Assembly);
      _fullName = type.FullName;
      _name = type.Name;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public bool Equals(TypeIdentity other)
   {
      return _assemblyIdentity.Equals(other._assemblyIdentity) &&
         _fullName == other._fullName &&
         _name == other._name;
   }

   /// <inheritdoc/>
   public override bool Equals(object? obj)
   {
      if (obj is TypeIdentity other)
         return Equals(other);

      return false;
   }

   /// <inheritdoc/>
   public override int GetHashCode() => HashCode.Combine(_assemblyIdentity, _fullName, _name);
   #endregion

   #region Operator overloads
   /// <summary>Checks whether <paramref name="a"/> and <paramref name="b"/> are the same.</summary>
   /// <returns>
   /// <see langword="true"/> if <paramref name="a"/> and <paramref name="b"/>
   /// are the same, otherwise <see langword="false"/>.
   /// </returns>
   public static bool operator ==(TypeIdentity a, TypeIdentity b) => a.Equals(b);

   /// <summary>Checks whether <paramref name="a"/> and <paramref name="b"/> are <b>not</b> the same.</summary>
   /// <returns>
   /// <see langword="true"/> if <paramref name="a"/> and <paramref name="b"/>
   /// are <b>not</b> the same, otherwise <see langword="false"/>.
   /// </returns>
   public static bool operator !=(TypeIdentity a, TypeIdentity b) => a.Equals(b) == false;
   #endregion
}
