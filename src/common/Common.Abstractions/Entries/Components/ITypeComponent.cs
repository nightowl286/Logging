using TNO.Logging.Common.Abstractions.LogData.Types;

namespace TNO.Logging.Common.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ComponentKind.Type"/> component.
/// </summary>
public interface ITypeComponent : IComponent
{
   #region Properties
   /// <summary>The id of the referenced <see cref="ITypeInfo"/>.</summary>
   ulong TypeId { get; }
   #endregion
}
