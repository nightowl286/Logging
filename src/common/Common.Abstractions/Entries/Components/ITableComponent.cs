using System.Collections.Generic;

namespace TNO.Logging.Common.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ComponentKind.Table"/>.
/// </summary>
public interface ITableComponent : IComponent
{
   #region Properties
   /// <summary>The table that contains the stored data.</summary>
   IReadOnlyDictionary<uint, object> Table { get; }
   #endregion
}
