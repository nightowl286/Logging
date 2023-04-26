using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Assembly"/> component.
/// </summary>
public class AssemblyComponent : IAssemblyComponent
{
   #region Properties
   /// <inheritdoc/>
   public ulong AssemblyId { get; }

   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Assembly;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="AssemblyComponent"/>.</summary>
   /// <param name="assemblyId">The id of the referenced <see cref="IAssemblyInfo"/>.</param>
   public AssemblyComponent(ulong assemblyId)
   {
      AssemblyId = assemblyId;
   }
   #endregion
}
