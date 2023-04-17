using System;

namespace TNO.Logging.Common.Abstractions.Versioning;

/// <summary>
/// Specifies a version on the assigned type.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class VersionAttribute : Attribute
{
   #region Properties
   /// <summary>The version specified by this attribute.</summary>
   public uint Version { get; }
   #endregion

   #region Constructor
   /// <summary>Creates a new instance of the <see cref="VersionAttribute"/>.</summary>
   /// <param name="verion">The version to assign.</param>
   public VersionAttribute(uint verion) => Version = verion;
   #endregion
}
