using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Types;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Type"/> component.
/// </summary>
public class TypeComponent : ITypeComponent
{
   #region Properties
   /// <inheritdoc/>
   public ulong TypeId { get; }

   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Type;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TypeComponent"/>.</summary>
   /// <param name="typeId">The id of the referenced <see cref="ITypeInfo"/>.</param>
   public TypeComponent(ulong typeId)
   {
      TypeId = typeId;
   }
   #endregion
}
