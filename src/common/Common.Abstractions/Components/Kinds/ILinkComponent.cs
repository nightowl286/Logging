namespace TNO.Common.Abstractions.Components.Kinds;
public interface ILinkComponent : IEntryComponent
{
   #region Properties
   ulong IdOfLinkedEntry { get; }
   #endregion
}
