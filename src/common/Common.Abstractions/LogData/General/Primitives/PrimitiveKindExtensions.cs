using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace TNO.Logging.Common.Abstractions.LogData.General.Primitives;

/// <summary>
/// Contains useful extension methods related to the <see cref="PrimitiveKind"/> and the <see cref="PrimitiveAttribute"/>.
/// </summary>
public static class PrimitiveKindExtensions
{
   #region Fields
   private static readonly Dictionary<PrimitiveKind, PrimitiveAttribute?> AttributeCache = new Dictionary<PrimitiveKind, PrimitiveAttribute?>();
   #endregion

   #region Methods
   /// <summary>Tries to get the <see cref="PrimitiveAttribute"/> for the given <paramref name="kind"/>.</summary>
   /// <param name="kind">The kind to get the <paramref name="attribute"/> for.</param>
   /// <param name="attribute">The <see cref="PrimitiveAttribute"/> for the given <paramref name="kind"/>, or <see langword="null"/>.</param>
   /// <returns><see langword="true"/> if the <paramref name="attribute"/> could be found, <see langword="false"/> otherwise.</returns>
   public static bool TryGetAttribute(this PrimitiveKind kind, [NotNullWhen(true)] out PrimitiveAttribute? attribute)
   {
      bool isDefined;
#if NET5_0_OR_GREATER
      isDefined = Enum.IsDefined(kind);
#else
      isDefined = Enum.IsDefined(typeof(PrimitiveKind), kind);
#endif

      if (isDefined == false)
      {
         attribute = null;
         return false;
      }

      lock (AttributeCache)
      {
         if (AttributeCache.TryGetValue(kind, out attribute) == false)
         {
            attribute = typeof(PrimitiveKind)
               .GetField(kind.ToString())
               ?.GetCustomAttribute<PrimitiveAttribute>();

            AttributeCache.Add(kind, attribute);
         }
      }

      return attribute is not null;
   }

   /// <summary>Tries to get the <see cref="PrimitiveAttribute"/> and the primitive <paramref name="kind"/> for the given <paramref name="type"/>.</summary>
   /// <param name="type">The type to get the kind and attribute for.</param>
   /// <param name="kind">The <see cref="PrimitiveKind"/> that the <paramref name="attribute"/> for the given <paramref name="type"/> was found on.</param>
   /// <param name="attribute">The <see cref="PrimitiveAttribute"/> that was found for the given <paramref name="type"/>.</param>
   /// <returns>
   /// <see langword="true"/> if an <paramref name="attribute"/> could be found
   /// for the given <paramref name="type"/>, <see langword="false"/> otherwise.
   /// </returns>
   public static bool TryGetPrimitiveKind(
      this Type type,
      [NotNullWhen(true)] out PrimitiveKind? kind,
      [NotNullWhen(true)] out PrimitiveAttribute? attribute)
   {
#if NET5_0_OR_GREATER
      foreach (PrimitiveKind value in Enum.GetValues<PrimitiveKind>())
      {
         if (value.TryGetAttribute(out attribute) && attribute.Type == type)
         {
            kind = value;
            return true;
         }
      }
#else
      foreach (PrimitiveKind value in Enum.GetValues(typeof(PrimitiveKind)))
      {
         if (value.TryGetAttribute(out attribute) && attribute.Type == type)
         {
            kind = value;
            return true;
         }
      }
#endif

      kind = null;
      attribute = null;
      return false;
   }
   #endregion
}
