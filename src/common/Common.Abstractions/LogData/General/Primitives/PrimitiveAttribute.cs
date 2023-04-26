using System;

namespace TNO.Logging.Common.Abstractions.LogData.General.Primitives;

/// <summary>
/// Represents an attribute that specifies the type and a size that a <see cref="PrimitiveKind"/> is.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class PrimitiveAttribute : Attribute
{
   #region Properties
   /// <summary>The type that the <see cref="PrimitiveKind"/> represents.</summary>
   public Type Type { get; }

   /// <summary>The size (if known, or <c>-1</c>) in bytes that the <see cref="PrimitiveKind"/> is.</summary>
   public int Size { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="PrimitiveAttribute"/>.</summary>
   /// <param name="type">The type that the <see cref="PrimitiveKind"/> represents.</param>
   /// <param name="size">The size (if known, or <c>-1</c>) in bytes that the <see cref="PrimitiveKind"/> is.</param>
   public PrimitiveAttribute(Type type, int size = -1)
   {
      Type = type;
      Size = size;
   }
   #endregion
}
