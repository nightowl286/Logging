using System;

namespace TNO.Logging.Common.Abstractions;

/// <summary>
/// Represents an attribute that allows for specifying the <see cref="VersionedDataKind"/> that a class/interface handles.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
public sealed class VersionedDataKindAttribute : Attribute
{
   #region Properties
   /// <summary>The data kind that the class/interface handles.</summary>
   public VersionedDataKind Kind { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="VersionedDataKindAttribute"/>.</summary>
   /// <param name="kind">The data kind that the class/interface handles.</param>
   public VersionedDataKindAttribute(VersionedDataKind kind) => Kind = kind;
   #endregion
}
