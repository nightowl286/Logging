using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Types;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Type"/> component.
/// </summary>
/// <param name="TypeId">The id of the referenced <see cref="ITypeInfo"/>.</param>
public record class TypeComponent(ulong TypeId) : ITypeComponent
{
   #region Properties
   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Type;
   #endregion
}
