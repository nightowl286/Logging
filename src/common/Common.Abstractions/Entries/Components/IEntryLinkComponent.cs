namespace TNO.Logging.Common.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ComponentKind.EntryLink"/> component.
/// </summary>
public interface IEntryLinkComponent : IComponent
{
   #region Properties
   /// <summary>The linked <see cref="IEntry.Id"/>.</summary>
   ulong EntryId { get; }
   #endregion
}
