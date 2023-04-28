using TNO.Logging.Common.Abstractions.LogData.General.Primitives;

namespace TNO.Logging.Common.LogData.Tables;

/// <summary>
/// Represents info about an unknown primitive value.
/// </summary>
public class UnknownPrimitive : IUnknownPrimitive, IEquatable<UnknownPrimitive>
{
   #region Properties
   /// <inheritdoc/>
   public ulong TypeId { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="UnknownPrimitive"/>.</summary>
   /// <param name="typeId">The type id of the <see cref="Type"/> that the value was of.</param>
   public UnknownPrimitive(ulong typeId)
   {
      TypeId = typeId;
   }

   #endregion

   #region Methods
   /// <inheritdoc/>
   public bool Equals(UnknownPrimitive? other)
   {
      if (other is null) return false;

      return TypeId == other.TypeId;
   }

   /// <inheritdoc/>
   public override bool Equals(object? obj)
   {
      if (obj is UnknownPrimitive other)
         return Equals(other);

      return false;
   }

   /// <inheritdoc/>
   public override int GetHashCode() => TypeId.GetHashCode();
   #endregion
}
