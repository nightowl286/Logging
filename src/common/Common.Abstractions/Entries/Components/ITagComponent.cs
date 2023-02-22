namespace TNO.Logging.Common.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ComponentKind.Tag"/> component.
/// </summary>
public interface ITagComponent : IComponent
{
   #region Properties
   /// <summary>The id of the referenced tag.</summary>
   ulong TagId { get; }
   #endregion
}
