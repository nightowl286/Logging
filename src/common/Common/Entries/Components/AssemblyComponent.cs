using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Assembly"/> component.
/// </summary>
/// <param name="AssemblyId">The id of the referenced <see cref="IAssemblyInfo"/>.</param>
public record class AssemblyComponent(ulong AssemblyId) : IAssemblyComponent
{
   #region Properties
   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Assembly;
   #endregion
}
