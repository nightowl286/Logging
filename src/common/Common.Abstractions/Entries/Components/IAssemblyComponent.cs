using TNO.Logging.Common.Abstractions.LogData.Assemblies;

namespace TNO.Logging.Common.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ComponentKind.Assembly"/> component.
/// </summary>
public interface IAssemblyComponent : IComponent
{
   #region Properties
   /// <summary>The id of the referenced <see cref="IAssemblyInfo"/>.</summary>
   ulong AssemblyId { get; }
   #endregion
}
