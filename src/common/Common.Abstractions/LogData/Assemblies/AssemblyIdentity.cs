using System;
using System.Diagnostics;
using System.Reflection;

namespace TNO.Logging.Common.Abstractions.LogData.Assemblies;

/// <summary>
/// Represents the identity of an <see cref="Assembly"/>.
/// </summary>
public readonly struct AssemblyIdentity : IEquatable<AssemblyIdentity>
{
   #region Fields
   private readonly string? _name;
   private readonly string? _cultureName;
   private readonly string _location;
   private readonly byte[]? _keyToken;

   private readonly int _versionMajor;
   private readonly int _versionMinor;
   private readonly int _versionBuild;
   private readonly int _versionRevision;
   #endregion

   #region Constructors
   /// <summary>Gets the identity of the given <paramref name="assembly"/>.</summary>
   /// <param name="assembly">The assembly to get the identity of.</param>
   public AssemblyIdentity(Assembly assembly)
      : this(assembly.GetName(), assembly.Location)
   {
   }

   /// <summary>Gets an identity for an <see cref="Assembly"/> based on its <paramref name="location"/> the <paramref name="assemblyName"/>.</summary>
   /// <param name="assemblyName">The name of the assembly.</param>
   /// <param name="location">The location of the assembly.</param>
   public AssemblyIdentity(AssemblyName assemblyName, string location)
   {
      _name = assemblyName.Name;
      _cultureName = assemblyName.CultureName;
      _location = location;

      byte[]? keyToken = assemblyName.GetPublicKeyToken();
      if (keyToken != null)
      {
         _keyToken = new byte[keyToken.Length];
         Array.Copy(keyToken, _keyToken, keyToken.Length);
      }

      Version? version = assemblyName.Version;

      _versionMajor = version?.Major ?? -1;
      _versionMinor = version?.Minor ?? -1;
      _versionBuild = version?.Build ?? -1;
      _versionRevision = version?.Revision ?? -1;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public bool Equals(AssemblyIdentity other)
   {
      return
         (_name == other._name) &&
         (_cultureName == other._cultureName) &&
         (_location == other._location) &&
         (_versionMajor == other._versionMajor) &&
         (_versionMinor == other._versionMinor) &&
         (_versionBuild == other._versionBuild) &&
         (_versionRevision == other._versionRevision) &&
         SameToken(other._keyToken);
   }

   /// <inheritdoc/>
   public override bool Equals(object? obj)
   {
      if (obj is AssemblyIdentity other)
         return Equals(other);

      return false;
   }

   /// <inheritdoc/>
   public override int GetHashCode()
   {
      HashCode hash = new HashCode();

      hash.Add(_name);
      hash.Add(_cultureName);
      hash.Add(_location);

      hash.Add(_versionMajor);
      hash.Add(_versionMinor);
      hash.Add(_versionBuild);
      hash.Add(_versionRevision);

      if (_keyToken is null)
         hash.Add(0);
#if NET6_0_OR_GREATER
      else
         hash.AddBytes(_keyToken);
#else
      else
      {
         foreach (byte b in _keyToken)
            hash.Add(b);
      }
#endif

      return hash.ToHashCode();
   }
   #endregion

   #region Helpers
   private bool SameToken(byte[]? token)
   {
      bool currentNull = _keyToken == null;
      bool otherNull = token == null;

      if (currentNull && otherNull)
         return true;

      if (currentNull ^ otherNull)
         return false;

      Debug.Assert(_keyToken is not null);
      Debug.Assert(token is not null);

      if (_keyToken.Length != token.Length)
         return false;

      for (int i = 0; i < token.Length; i++)
      {
         if (token[i] != _keyToken[i])
            return false;
      }

      return true;
   }
   #endregion

   #region Operator overloads
   /// <summary>Checks whether <paramref name="a"/> and <paramref name="b"/> are the same.</summary>
   /// <returns><see langword="true"/> if <paramref name="a"/> and <paramref name="b"/> are the same, otherwise <see langword="false"/>.</returns>
   public static bool operator ==(AssemblyIdentity a, AssemblyIdentity b) => a.Equals(b);

   /// <summary>Checks whether <paramref name="a"/> and <paramref name="b"/> are <b>not</b> the same.</summary>
   /// <returns><see langword="true"/> if <paramref name="a"/> and <paramref name="b"/> are <b>not</b> the same, otherwise <see langword="false"/>.</returns>
   public static bool operator !=(AssemblyIdentity a, AssemblyIdentity b) => a.Equals(b) == false;
   #endregion
}
